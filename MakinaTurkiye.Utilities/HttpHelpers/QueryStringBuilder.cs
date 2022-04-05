using System;
using System.Collections.Generic;
using System.Text;

namespace MakinaTurkiye.Utilities.HttpHelpers
{
    public static class QueryStringBuilder
    {

        public static string ModifyQueryString(string url, string queryStringModification, string anchor)
        {
            if (url == null)
                url = string.Empty;
            //url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            //queryStringModification = queryStringModification.ToLowerInvariant();

            if (anchor == null)
                anchor = string.Empty;
            anchor = anchor.ToLowerInvariant();


            string str = string.Empty;
            string str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#") + 1);
                url = url.Substring(0, url.IndexOf("#"));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                if (!dictionary.ContainsKey(strArray[0]))
                                {
                                    dictionary[strArray[0]] = strArray[1];
                                }
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    foreach (string str4 in queryStringModification.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            string[] strArray2 = str4.Split(new char[] { '=' });
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }
            if (!string.IsNullOrEmpty(anchor))
            {
                str2 = anchor;
            }
            //return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2)));
        }

        public static string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
                url = string.Empty;
            //url = url.ToLowerInvariant();

            if (queryString == null)
                queryString = string.Empty;
            //queryString = queryString.ToLowerInvariant();


            string str = string.Empty;
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }
            if (!string.IsNullOrEmpty(queryString))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split(new char[] { '&' }))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split(new char[] { '=' });
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    dictionary.Remove(queryString);

                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
            }
            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)));
        }


        public static string ModifyActivityTypeFilterQueryString(string url, string value)
        {
            const string filterName = "filtre=";

            var filterIndex = url.IndexOf(filterName, StringComparison.Ordinal);
            if (!url.Contains("?"))
            {
                return ModifyQueryString(url, filterName + value, null);
            }
            if (!url.Contains(filterName))
            {
                return ModifyQueryString(url, filterName + value, null);
            }

            if (url.Contains("&"))
            {

                if (url.IndexOf("&", StringComparison.Ordinal) > filterIndex)
                {
                    var substr = url.Substring(filterIndex + filterName.Length, url.IndexOf("&", StringComparison.Ordinal) - (filterIndex + filterName.Length));
                    var insert = url.Insert(filterIndex + filterName.Length + substr.Length, "," + value);
                    return insert;
                }
                if (url.IndexOf("&", StringComparison.Ordinal) == url.LastIndexOf("&", StringComparison.Ordinal))
                {
                    var substr = url.Substring(filterIndex + filterName.Length, url.Length - (filterIndex + filterName.Length));
                    var insert = url.Insert(filterIndex + filterName.Length + substr.Length, "," + value);
                    return insert;
                }
                else
                {
                    int total = filterIndex + filterName.Length;
                    int another = url.Length - (filterIndex + filterName.Length);
                    //int another = (filterIndex + filterName.Length) - url.LastIndexOf("&", StringComparison.Ordinal)+filterName.Length;
                    var substr = url.Substring(total, another);

                    var insert = url.Insert(filterIndex + filterName.Length, value + ",");
                    return insert;
                }
            }
            else
            {
                var substr = url.Substring(filterIndex + filterName.Length, url.Length - (filterIndex + filterName.Length));
                var insert = url.Insert(filterIndex + filterName.Length + substr.Length, "," + value);
                return insert;
            }
        }

        public static string RemoveActivityTypeFilterQueryString(string url, string value)
        {
            var index = url.IndexOf(value, StringComparison.Ordinal);
            var right = index + value.Length >= url.Length ? char.MinValue : url[index + value.Length];
            const string filterText = "filtre=";
            var filterTextLeft = url[url.IndexOf(filterText, StringComparison.Ordinal) - 1];

            var left = url[index - 1];
            var newUrl = "";
            if (left == ',' && right == ',')
            {
                newUrl = url.Replace(left + value, string.Empty);
            }
            else if (left == '=' && right == char.MinValue)
            {
                newUrl = url.Replace(filterTextLeft + filterText + value, string.Empty);
            }
            else if (left == '=' && right == ',')
            {
                newUrl = url.Replace(value + right, string.Empty);
            }
            else if (left == '=' && right == '&')
            {
                var temp = filterTextLeft == '?' ? "?" : "&";
                newUrl = url.Replace(filterTextLeft + filterText + value + right, temp);
            }
            else if (left == ',' && right == '&')
            {
                newUrl = url.Replace(left + value, string.Empty);
            }
            else if (left == ',' && right == char.MinValue)
            {
                newUrl = url.Replace(left + value, string.Empty);

            }
            return newUrl;
        }
        /// <summary>
        /// Boþluklarý '-' ile gelen activity isimlerini düzgün hale çevirir
        /// </summary>
        /// <param name="activityTypeNames"></param>
        /// <returns></returns>
        public static string GetCorrectlyActivityTypeName(string activityTypeNames)
        {
            var list = activityTypeNames.Split(',');
            var newArray = new List<string>();
            foreach (var name in list)
            {
                if (!name.Contains("---"))
                {
                    newArray.Add(name.Replace("-", " "));
                }
                else
                {
                    var temp = string.Empty;
                    var hypenIndexOf = name.IndexOf("---", StringComparison.Ordinal);
                    var leftHypen = hypenIndexOf;
                    var centertHypen = hypenIndexOf + 1;
                    var rightHypen = hypenIndexOf + 2;
                    temp = name.Remove(leftHypen, 1).Insert(leftHypen, " ");
                    temp = name.Remove(rightHypen, 1).Insert(rightHypen, " ");
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (i != centertHypen && temp[i] == '-')
                        {
                            temp = temp.Remove(i, 1).Insert(i, " ");
                        }
                    }
                    newArray.Add(temp);
                }
            }
            return string.Join(",", newArray);
        }

    }
}
