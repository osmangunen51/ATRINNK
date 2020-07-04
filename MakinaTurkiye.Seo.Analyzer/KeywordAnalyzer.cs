using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MakinaTurkiye.Seo.Analyzer
{
	public class KeywordAnalyzer
	{
        public List<string> ClearList { get; set; } = new List<string>() {
            "Google Tag Manager ",
            "<!-- Cookie Policy File --> <!-- Cookie Policy File -->",
            "[*]",
            "--"
        };
        public KeywordAnalysis Analyze(string content,bool Statistics=false)
		{
            content = content.ToLower();
            foreach (var item in ClearList)
            {
                content = content.Replace(item.ToLower(), "");
            }
			KeywordAnalysis analysis = new KeywordAnalysis { Content = content };
			int wordCount = 0;
		    var titles = TitleExtractor.Extract(content);
			var paragraphs = WordScraper.ScrapeToParagraphs(content, out wordCount);

			//flatten list of words
			List<Word> allWords = new List<Word>();
			paragraphs.ForEach(p => p.Sentences.ForEach(s => allWords.AddRange(s.Words)));

            analysis.WordCount = wordCount;
			analysis.Paragraphs = paragraphs;
			analysis.Titles = titles;
			//run through each sentence and grab two and three word segments and add them to the termCount
			Dictionary<string, int> termOccurrenceCounts = GetWordTermOccurence(paragraphs);

			

            analysis.Keywords = from n in termOccurrenceCounts
                                select new Keyword
                                {
                                    Word = n.Key,
                                    Count = n.Value
                                };

            List<int> PhraseListesi = new List<int>();
            foreach (var Item in analysis.Keywords)
            {
                
                // Tek Kelime İçin yazıldı
                if (Item.Word.Trim().Length < 4)
                {
                    continue;
                }

                // 2 ve daha fazlası için yazıldı ama şuan kaldırıldı.
                //bool Cikis = false;
                //foreach (var It in Item.Word.Split(' '))
                //{
                //    if (It.Trim().Length<4)
                //    {
                //        Cikis = true;
                //        continue;
                //    }
                //}
                //if (Cikis)
                //{
                //    continue;
                //}
                int PhraseCount =(int)GetPhraseCount(Item.Word);
                Phare Phr = new Phare();
                if (analysis.Phares.Where(x => x.PhareKey == PhraseCount).Count()> 0)
                {
                    Phr = analysis.Phares.Where(x => x.PhareKey == PhraseCount).FirstOrDefault();
                    Phr.Count++;
                    Phr.Liste.Add(Item);
                }
                else
                {
                    Phr.PhareKey = PhraseCount;
                    Phr.Name = string.Format("Kelime {0}", PhraseCount + 1);
                    Phr.Count++;
                    Phr.Liste.Add(Item);
                    analysis.Phares.Add(Phr);
                }
            }

            foreach (var Phare in analysis.Phares)
            {
                Phare.Count = Phare.Liste.Count();
                foreach (var Wrd in Phare.Liste)
                {
                    Wrd.Rate = CallRate(Wrd.Count, Phare.Count);
                }
                Phare.Liste = Phare.Liste.OrderByDescending(x => x.Rate).ToList();
            }
            return analysis;
		}

        public decimal CallRate(decimal Nesne, decimal Nesne2)
        {
            decimal Sonuc = 0;
            if (Nesne < 0)
            {
                Nesne = Nesne * -1;
            }
            if (Nesne > 0)
            {
                Sonuc = Math.Round(((Nesne * 100) / Nesne2), 2);
            }
            return Sonuc;
        }


        public decimal GetPhraseCount(string Txt)
        {
            for (int Don = 0; Don < 25; Don++)
            {
                Txt = Txt.Replace("  ", " ");
            }
            return Txt.Count(Char.IsWhiteSpace);
        }

        private string GetTermFromStemTerm(List<Word> allWords, string term)
		{
			if (term.IndexOf(" ") > -1)
			{
				string[] terms = term.Split(' ');
				string[] words = new string[terms.Length];
				for (int i = 0; i < terms.Length; i++)
				{
					words[i] = GetTermFromStem(allWords, terms[i]);
				}
				string retval = string.Join(" ", words);
				return retval;
			}
			else
			{
				return GetTermFromStem(allWords, term);
			}
		}

		private string GetTermFromStem(List<Word> allWords, string stem)
		{
			var words = (from n in allWords where n.Stem == stem select n).ToList();
			if (words.Count > 0)
			{
				var w = from n in words
						  group n by n.Text into grp
						  select new { Text = grp.Key, Count = grp.Select(x => x.Text).Distinct().Count() };

				var top = (from n in w orderby n.Count descending select n).First();

				return top.Text;
			}
			else
				return string.Empty;

			//if (stems.ContainsKey(stem))
			//{
			//   Dictionary<string, int> words = stems[stem];
			//   string word = string.Empty;
			//   int count = 0;
			//   foreach (KeyValuePair<string, int> pair in words)
			//   {
			//      if (pair.Value > count)
			//      {
			//         word = pair.Key;
			//         count = pair.Value;
			//      }
			//   }
			//   return word;
			//}
			//else
			//   return string.Empty;
		}

		private Dictionary<string, Dictionary<string, decimal>> FillTermFwgCollection(List<Paragraph> paragraphs, 
			SortedDictionary<decimal, string> termsG)
		{
			//termFwg
			// * Fwg = sentence count where w and g occur divided by the total number of sentences (sentenceCount)
			// *       = termFwg is Dictionary<string, Dictionary<string, decimal>>
			Dictionary<string, Dictionary<string, decimal>> termFwg = new Dictionary<string, Dictionary<string, decimal>>();
			int sentenceCount = (from n in paragraphs select n.Sentences).Count();

			string[] terms = new string[termsG.Count];
			foreach (string w in termsG.Values.ToArray())
			{
				foreach (KeyValuePair<decimal, string> pair in termsG)
				{
					string g = pair.Value;
					if (g != w)
					{
						int sentCountWG = 0;
						foreach (var paragraph in paragraphs)
						{
							foreach (var sentence in paragraph.Sentences)
							{
								if (TermsCoOccur(sentence, w, g)) sentCountWG++;
							}
						}
						decimal Fwg = sentCountWG > 0 ? sentCountWG / (decimal)sentenceCount : 0.0m;
						if (!termFwg.ContainsKey(w))
							termFwg.Add(w, new Dictionary<string, decimal>()); //add if not there yet
						termFwg[w].Add(g, Fwg);
					}
				}
			}
			return termFwg;
		}

		private bool TermsCoOccur(Sentence sentence, string w, string g)
		{
			if (TermInSentence(sentence, w) && TermInSentence(sentence, g))
				return true;
			else
				return false;
		}

		private bool TermInSentence(Sentence sentence, string term)
		{
			bool found = false;
			//if term appears in this sentence, count the terms (words + 2 and 3 word terms)
			if (term.IndexOf(" ") > -1)
			{
				string[] termWords = term.Split(' ');
				for (int i = 0; i < sentence.Words.Count; i++)
				{
					var t = sentence.Words[i];
					if (termWords.Length == 2 && i > 2)
					{
						var t1 = sentence.Words[i - 1];
						if (termWords[0] == t1.Stem && termWords[1] == t.Stem)
						{
							found = true;
							break;
						}
					}
					else if (termWords.Length == 3 && i > 3)
					{
						var t1 = sentence.Words[i - 1];
						var t2 = sentence.Words[i - 2];
						if (termWords[0] == t2.Stem && termWords[1] == t1.Stem && termWords[2] == t.Stem)
						{
							found = true;
							break;
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < sentence.Words.Count; i++)
				{
					var t = sentence.Words[i];
					if (t.Stem == term)
					{
						found = true;
						break;
					}
				}
			}
			return found;
		}

		private Dictionary<string, decimal> FillTermPgNwCollections(List<Paragraph> paragraphs,
			SortedDictionary<decimal, string> termsG, ref Dictionary<string, int> termNw, ref int termTotal)
		{
			//termPg
			// * Pg = sum of the total number of terms in sentences where  
			// *      g appears divided by the total number of terms in the document (termTotal)
			// total number of terms in sentence = word count + # of 2 and 3 word combos = termsInSentencesForTerm
			Dictionary<string, decimal> termPg = new Dictionary<string, decimal>();

			foreach (KeyValuePair<decimal, string> pair in termsG)
			{
				string term = pair.Value;
				int termsInSentencesForTerm = 0;
				foreach (var paragraph in paragraphs)
				{
					foreach (var sentence in paragraph.Sentences)
					{
						bool found = false;
						//if term appears in this sentence, count the terms (words + 2 and 3 word terms)
						if (term.IndexOf(" ") > -1)
						{
							string[] termWords = term.Split(' ');
							for (int i = 0; i < sentence.Words.Count; i++)
							{
								var t = sentence.Words[i];
								if (termWords.Length == 2 && i > 2)
								{
									var t1 = sentence.Words[i - 1];
									if (termWords[0] == t1.Stem && termWords[1] == t.Stem)
									{
										found = true;
										break;
									}
								}
								else if (termWords.Length == 3 && i > 3)
								{
									var t1 = sentence.Words[i - 1];
									var t2 = sentence.Words[i - 2];
									if (termWords[0] == t2.Stem && termWords[1] == t1.Stem && termWords[2] == t.Stem)
									{
										found = true;
										break;
									}
								}
							}
						}
						else
						{
							for (int i = 0; i < sentence.Words.Count; i++)
							{
								var t = sentence.Words[i];
								if (t.Stem == term)
								{
									found = true;
									break;
								}
							}
						}
						if (found)
						{
							//now get terms count (words + 2 and 3 word terms) and increment termsInSentencesForTerm
							termsInSentencesForTerm += sentence.Words.Count;
							if (sentence.Words.Count > 2) termsInSentencesForTerm += sentence.Words.Count - 2; //all three word terms
							if (sentence.Words.Count > 1) termsInSentencesForTerm += sentence.Words.Count - 1; //all two word terms
						}
					}
				}
				termNw.Add(term, termsInSentencesForTerm);
				decimal pg = termsInSentencesForTerm / (decimal)termTotal;
				termPg.Add(term, pg);
			} //end foreach in termsG
			return termPg;
		}

		private SortedDictionary<decimal, string> SortTermsIntoProbabilities(Dictionary<string, int> counts, 
			ref Dictionary<string, decimal> termsX2, ref int termTotal)
		{
			SortedDictionary<decimal, string> probabilityTerms = new SortedDictionary<decimal, string>();
			SortedDictionary<decimal, string> termsG = new SortedDictionary<decimal, string>();
			
			foreach (KeyValuePair<string, int> pair in counts)
			{
				termTotal += pair.Value;
			}
			decimal total = (decimal)termTotal;
			decimal probTotal = 0; //to be used for calculating the average probability
			foreach (KeyValuePair<string, int> pair in counts)
			{
				decimal prob = pair.Value / total;
				probTotal += prob;
				while (probabilityTerms.ContainsKey(prob))
				{
					prob = prob - 0.00001m; //offset by the slightest amount to get unique key
				}
				probabilityTerms.Add(prob, pair.Key);
			}
			decimal probAvg = counts.Count > 0 ? probTotal / counts.Count : 0;

			//only take the top 10% up to the top 30 terms and if top 10% is less than 10 then take up to 5
			int toptenCount = counts.Count;
			//if (toptenCount > 30)
			//	toptenCount = 30;
			//else if (toptenCount < 10)
			//	toptenCount = 5;

			if (toptenCount > counts.Count) toptenCount = counts.Count; //just in case there are so few

			decimal[] ptkey = new decimal[probabilityTerms.Count];
			probabilityTerms.Keys.CopyTo(ptkey, 0);

			for (int i = ptkey.Length - 1; i > ptkey.Length - toptenCount - 1; i--)
			{
				decimal key = ptkey[i];
				string val = probabilityTerms[key];
				termsG.Add(key, val);
				termsX2.Add(val, 0); //initializes the list for storing X2 calculation results to be sorted later
			}
			return termsG;
		}

		private Dictionary<string, int> GetWordTermOccurence(List<Paragraph> paragraphs)
		{
			Dictionary<string, int> counts = new Dictionary<string, int>();
			foreach (var p in paragraphs)
			{
				foreach (var s in p.Sentences)
				{
					for (int i = 0; i < s.Words.Count; i++)
					{
						Word w = s.Words[i];
						CountTerm(counts, w.Stem);
						if (i > 0) //we can have a two word phrase
						{
							Word tm1 = s.Words[i - 1];
							string term = tm1.Stem + " " + w.Stem;
							CountTerm(counts, term);
						}
						if (i > 1) //we can have a three word phrase
						{
							Word tm1 = s.Words[i - 1];
							Word tm2 = s.Words[i - 2];
							string term = tm2.Stem + " " + tm1.Stem + " " + w.Stem;
							CountTerm(counts, term);
						}
					}
				}
			}
			return counts;
		}

		private void CountTerm(Dictionary<string, int> counts, string stem)
		{
			if (counts.ContainsKey(stem))
				counts[stem]++;
			else
				counts.Add(stem, 1);
		}
	}
}
