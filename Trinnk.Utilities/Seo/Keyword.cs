using System.Collections.Generic;

namespace Trinnk.Utilities.Seo
{
    public class KeywordAnalysis
    {
        public string Content { get; set; }
        public int WordCount { get; set; }
        public IEnumerable<Keyword> Keywords { get; set; }
        public List<Paragraph> Paragraphs { get; set; }
        public IEnumerable<Title> Titles { get; set; }
        public List<Phare> Phares { get; set; } = new List<Phare>();
    }

    public class Phare
    {
        public int PhareKey { get; set; } = -1;
        public string Name { get; set; } = "Kelime";
        public List<Keyword> Liste { get; set; } = new List<Keyword>();
        public int Count { get; set; } = 0;
    }

    public class Keyword
    {
        public string Word { get; set; } = "";
        public decimal Rank { get; set; } = 0;
        public decimal Count { get; set; } = 0;
        public decimal Rate { get; set; } = 0;
    }

    public class Word
    {
        public string Text { get; set; }
        public string Stem { get; set; }
    }

    public class Sentence
    {
        public List<Word> Words { get; set; }

        public Sentence() { Words = new List<Word>(); }
    }

    public class Paragraph
    {
        public List<Sentence> Sentences { get; set; }

        public Paragraph() { Sentences = new List<Sentence>(); }
    }
}
