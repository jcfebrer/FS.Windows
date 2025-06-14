#region

using System.Collections.Specialized;
using System.Text;

#endregion

namespace FSFormControls
{
    internal class DBHtmlParser
    {
        private static readonly char[] WHITESPACE_CHARS = " \r\n\t".ToCharArray();


        public bool RemoveEmptyElementText { get; set; }

        #region '"HTML tokeniser"' 

        private StringCollection GetTokens(string input)
        {
            var tokens = new StringCollection();

            var i = 0;
            var status = ParseStatus.ReadText;

            while (i < input.Length)
                if (status == ParseStatus.ReadText)
                {
                    if (i + 2 < input.Length && input.Substring(i, 2).Equals("</"))
                    {
                        i += 2;
                        tokens.Add("</");
                        status = ParseStatus.ReadEndTag;
                    }
                    else if (input.Substring(i, 1).Equals("<"))
                    {
                        i = i + 1;
                        tokens.Add("<");
                        status = ParseStatus.ReadStartTag;
                    }
                    else
                    {
                        var next_index = input.IndexOf("<", i);
                        if (next_index == -1)
                        {
                            tokens.Add(input.Substring(i));
                            break;
                        }

                        tokens.Add(input.Substring(i, next_index - i));
                        i = next_index;
                    }
                }
                else if (status == ParseStatus.ReadStartTag)
                {
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                    var tag_name_start = i;
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t/>".ToCharArray()) == -1)
                        i = i + 1;
                    tokens.Add(input.Substring(tag_name_start, i - tag_name_start));
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                    if (i + 1 < input.Length && input.Substring(i, 1).Equals("/>"))
                    {
                        tokens.Add("/>");
                        status = ParseStatus.ReadText;
                        i += 2;
                    }
                    else if (i < input.Length && input.Substring(i, 1).Equals(">"))
                    {
                        tokens.Add(">");
                        status = ParseStatus.ReadText;
                        i = i + 1;
                    }
                    else
                    {
                        status = ParseStatus.ReadAttributeName;
                    }
                }
                else if (status == ParseStatus.ReadEndTag)
                {
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                    var tag_name_start = i;
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t>".ToCharArray()) == -1)
                        i = i + 1;
                    tokens.Add(input.Substring(tag_name_start, i - tag_name_start));
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                    if (i < input.Length && input.Substring(i, 1).Equals(">"))
                    {
                        tokens.Add(">");
                        status = ParseStatus.ReadText;
                        i = i + 1;
                    }
                }
                else if (status == ParseStatus.ReadAttributeName)
                {
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                    var attribute_name_start = i;
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t/>=".ToCharArray()) == -1)
                        i = i + 1;
                    tokens.Add(input.Substring(attribute_name_start, i - attribute_name_start));
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                    if (i + 1 < input.Length && input.Substring(i, 2).Equals("/>"))
                    {
                        tokens.Add("/>");
                        status = ParseStatus.ReadText;
                        i += 2;
                    }
                    else if (i < input.Length && input.Substring(i, 1).Equals(">"))
                    {
                        tokens.Add(">");
                        status = ParseStatus.ReadText;
                        i = i + 1;
                    }
                    else if (i < input.Length && input.Substring(i, 1).Equals("="))
                    {
                        tokens.Add("=");
                        i = i + 1;
                        status = ParseStatus.ReadAttributeValue;
                    }
                    else if (i < input.Length && input.Substring(i, 1).Equals("/"))
                    {
                        i = i + 1;
                    }
                }
                else if (status == ParseStatus.ReadAttributeValue)
                {
                    while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                    if (i < input.Length && input.Substring(i, 1).Equals(@""""))
                    {
                        var value_start = i;
                        i = i + 1;
                        while (i < input.Length && !input.Substring(i, 1).Equals(@"""")) i = i + 1;
                        if (i < input.Length && input.Substring(i, 1).Equals(@"""")) i = i + 1;
                        tokens.Add(input.Substring(value_start + 1, i - value_start - 2));
                        status = ParseStatus.ReadAttributeName;
                    }
                    else if (i < input.Length && input.Substring(i, 1).Equals("'"))
                    {
                        var value_start = i;
                        i = i + 1;
                        while (i < input.Length && !input.Substring(i, 1).Equals("'")) i = i + 1;
                        if (i < input.Length && input.Substring(i, 1).Equals("'")) i = i + 1;
                        tokens.Add(input.Substring(value_start + 1, i - value_start - 2));
                        status = ParseStatus.ReadAttributeName;
                    }
                    else
                    {
                        var value_start = i;
                        while (i < input.Length && input.Substring(i, 1).IndexOfAny(" \r\n\t/>".ToCharArray()) == -1)
                            i = i + 1;
                        tokens.Add(input.Substring(value_start, i - value_start));
                        while (i < input.Length && input.Substring(i, 1).IndexOfAny(WHITESPACE_CHARS) != -1) i = i + 1;
                        status = ParseStatus.ReadAttributeName;
                    }

                    if (i + 1 < input.Length && input.Substring(i, 2).Equals("/>"))
                    {
                        tokens.Add("/>");
                        status = ParseStatus.ReadText;
                        i += 2;
                    }
                    else if (i < input.Length && input.Substring(i, 1).Equals(">"))
                    {
                        tokens.Add(">");
                        i = i + 1;
                        status = ParseStatus.ReadText;
                    }
                }

            return tokens;
        }

        #endregion

        #region Nested type: ParseStatus

        private enum ParseStatus
        {
            ReadText = 0,
            ReadEndTag = 1,
            ReadStartTag = 2,
            ReadAttributeName = 3,
            ReadAttributeValue = 4
        }

        #endregion

        #region '"The main parser"' 

        public DBHtmlNodeCollection Parse(string html)
        {
            var nodes = new DBHtmlNodeCollection(null);

            html = PreprocessScript(html, "script");
            html = PreprocessScript(html, "style");

            html = RemoveComments(html);
            html = RemoveSGMLComments(html);
            var tokens = GetTokens(html);

            var index = 0;
            DBHtmlElement element = null;
            while (index < tokens.Count)
                if ("<".Equals(tokens[index]))
                {
                    index = index + 1;
                    if (index >= tokens.Count) return null;
                    var tag_name = tokens[index];
                    index = index + 1;
                    element = new DBHtmlElement(tag_name);

                    while (index < tokens.Count && !">".Equals(tokens[index]) && !"/>".Equals(tokens[index]))
                    {
                        var attribute_name = tokens[index];
                        index = index + 1;
                        if ((index < tokens.Count) & "=".Equals(tokens[index]))
                        {
                            index = index + 1;
                            string attribute_value = null;
                            if (index < tokens.Count)
                                attribute_value = tokens[index];
                            else
                                attribute_value = null;
                            index = index + 1;
                            var attribute = new DBHtmlAttribute(attribute_name,
                                DBHtmlEncoder.DecodeValue(attribute_value));
                            element.Attributes.Add(attribute);
                        }
                        else if (index < tokens.Count)
                        {
                            var attribute = new DBHtmlAttribute(attribute_name, null);
                            element.Attributes.Add(attribute);
                        }
                    }

                    nodes.Add(element);
                    if ((index < tokens.Count) & "/>".Equals(tokens[index]))
                    {
                        element.IsTerminated = true;
                        index = index + 1;
                        element = null;
                    }
                    else if ((index < tokens.Count) & ">".Equals(tokens[index]))
                    {
                        index = index + 1;
                    }
                }
                else
                {
                    if (">".Equals(tokens[index]))
                    {
                        index = index + 1;
                    }
                    else
                    {
                        if ("</".Equals(tokens[index]))
                        {
                            index = index + 1;
                            if (index >= tokens.Count) return null;
                            var tag_name = tokens[index];
                            index = index + 1;

                            var open_index = FindTagOpenNodeIndex(nodes, tag_name);
                            if (open_index != -1)
                                MoveNodesDown(ref nodes, open_index + 1, (DBHtmlElement) nodes[open_index]);

                            while ((index < tokens.Count) & !">".Equals(tokens[index])) index = index + 1;
                            if ((index < tokens.Count) & ">".Equals(tokens[index])) index = index + 1;

                            element = null;
                        }
                        else
                        {
                            var value = tokens[index];
                            if (RemoveEmptyElementText) value = RemoveWhitespace(value);
                            value = DecodeScript(value);

                            if (RemoveEmptyElementText & (value.Length == 0))
                            {
                            }
                            else
                            {
                                if (!(!(element == null) && element.NoEscaping))
                                    value = DBHtmlEncoder.DecodeValue(value);
                                var node = new DBHtmlText(value);
                                nodes.Add(node);
                            }

                            index = index + 1;
                        }
                    }
                }

            return nodes;
        }


        private void MoveNodesDown(ref DBHtmlNodeCollection nodes, int node_index, DBHtmlElement New_parent)
        {
            var i = 0;
            for (i = node_index; i <= nodes.Count - 1; i++)
            {
                New_parent.Nodes.Add(nodes[i]);
                nodes[i].SetParent(New_parent);
            }

            var c = nodes.Count;
            i = node_index;
            for (i = node_index; i <= c - 1; i++) nodes.RemoveAt(node_index);
            New_parent.IsExplicitlyTerminated = true;
        }


        private int FindTagOpenNodeIndex(DBHtmlNodeCollection nodes, string name)
        {
            var index = 0;
            for (index = nodes.Count - 1; index >= 0; index += -1)
                if (nodes[index] is DBHtmlElement)
                    if (((DBHtmlElement) nodes[index]).Name.ToLower().Equals(name.ToLower()) &
                        (((DBHtmlElement) nodes[index]).Nodes.Count == 0) &
                        (((DBHtmlElement) nodes[index]).IsTerminated == false))
                        return index;
            return -1;
        }

        #endregion

        #region '"HTML clean-up functions"' 

        private string RemoveWhitespace(string input)
        {
            var output = input.Replace(Global.Cr, "");
            output = output.Replace(Global.Lf, "");
            output = output.Replace(Global.Tab, " ");
            output = output.Trim();
            return output;
        }


        private string RemoveComments(string input)
        {
            var output = new StringBuilder();

            var i = 0;
            var inTag = false;

            while (i < input.Length)
                if (i + 4 < input.Length && input.Substring(i, 4).Equals("<!--"))
                {
                    i += 4;
                    i = input.IndexOf("-->", i);
                    if (i == -1) break;
                    i += 3;
                }
                else if (input.Substring(i, 1).Equals("<"))
                {
                    inTag = true;
                    output.Append("<");
                    i = i + 1;
                }
                else if (input.Substring(i, 1).Equals(">"))
                {
                    inTag = false;
                    output.Append(">");
                    i = i + 1;
                }
                else if (input.Substring(i, 1).Equals(@"""") & inTag)
                {
                    var string_start = i;
                    i = i + 1;
                    i = input.IndexOf(@"""", i);
                    if (i == -1) break;
                    i = i + 1;
                    output.Append(input.Substring(string_start, i - string_start));
                }
                else if (input.Substring(i, 1).Equals("'") & inTag)
                {
                    var string_start = i;
                    i = i + 1;
                    i = input.IndexOf("'", i);
                    if (i == -1) break;
                    i = i + 1;
                    output.Append(input.Substring(string_start, i - string_start));
                }
                else
                {
                    output.Append(input.Substring(i, 1));
                    i = i + 1;
                }

            return output.ToString();
        }


        private string RemoveSGMLComments(string input)
        {
            var output = new StringBuilder();

            var i = 0;
            var inTag = false;

            while (i < input.Length)
                if (i + 2 < input.Length && input.Substring(i, 2).Equals("<!"))
                {
                    i += 2;
                    i = input.IndexOf(">", i);
                    if (i == -1) break;
                    i += 3;
                }
                else if (input.Substring(i, 1).Equals("<"))
                {
                    inTag = true;
                    output.Append("<");
                    i = i + 1;
                }
                else if (input.Substring(i, 1).Equals(">"))
                {
                    inTag = false;
                    output.Append(">");
                    i = i + 1;
                }
                else if (input.Substring(i, 1).Equals(@"""") & inTag)
                {
                    var string_start = i;
                    i = i + 1;
                    i = input.IndexOf(@"""", i);
                    if (i == -1) break;
                    i = i + 1;
                    output.Append(input.Substring(string_start, i - string_start));
                }
                else if (input.Substring(i, 1).Equals("'") & inTag)
                {
                    var string_start = i;
                    i = i + 1;
                    i = input.IndexOf("'", i);
                    if (i == -1) break;
                    i = i + 1;
                    output.Append(input.Substring(string_start, i - string_start));
                }
                else
                {
                    output.Append(input.Substring(i, 1));
                    i = i + 1;
                }

            return output.ToString();
        }


        private string PreprocessScript(string input, string tag_name)
        {
            var output = new StringBuilder();
            var index = 0;
            var tag_name_len = tag_name.Length;
            while (index < input.Length)
            {
                var omit_body = false;
                if (index + tag_name_len + 1 < input.Length &&
                    input.Substring(index, tag_name_len + 1).ToLower().Equals("<" + tag_name))
                {
                    do
                    {
                        if (index >= input.Length) break;

                        if (input.Substring(index, 1).Equals(">"))
                        {
                            output.Append(">");
                            index = index + 1;
                            break;
                        }

                        if ((index + 1 < input.Length) & input.Substring(index, 2).Equals("/>"))
                        {
                            output.Append("/>");
                            index += 2;
                            omit_body = true;
                            break;
                        }

                        if (input.Substring(index, 1).Equals(@""""))
                        {
                            output.Append(@"""");
                            index = index + 1;
                            while ((index < input.Length) & !input.Substring(index, 1).Equals(@""""))
                            {
                                output.Append(input.Substring(index, 1));
                                index = index + 1;
                            }

                            if (index < input.Length)
                            {
                                index = index + 1;
                                output.Append(@"""");
                            }
                        }
                        else if (input.Substring(index, 1).Equals("'"))
                        {
                            output.Append("'");
                            index = index + 1;
                            while ((index < input.Length) & !input.Substring(index, 1).Equals("'"))
                            {
                                output.Append(input.Substring(index, 1));
                                index = index + 1;
                            }

                            if (index < input.Length)
                            {
                                index = index + 1;
                                output.Append("'");
                            }
                        }
                        else
                        {
                            output.Append(input.Substring(index, 1));
                            index = index + 1;
                        }
                    } while (true);

                    if (index >= input.Length) break;

                    if (!omit_body)
                    {
                        var script_body = new StringBuilder();
                        while ((index + tag_name_len + 3 < input.Length) &
                               !input.Substring(index, tag_name_len + 3).ToLower().Equals("</" + tag_name + ">"))
                        {
                            script_body.Append(input.Substring(index, 1));
                            index = index + 1;
                        }

                        output.Append(EncodeScript(script_body.ToString()));
                        output.Append("</" + tag_name + ">");
                        if (index + tag_name_len + 3 < input.Length) index += tag_name_len + 3;
                    }
                }
                else
                {
                    output.Append(input.Substring(index, 1));
                    index = index + 1;
                }
            }

            return output.ToString();
        }


        private string EncodeScript(string script)
        {
            var output = script.Replace("<", "[MIL-SCRIPT-LT]");
            output = output.Replace(">", "[MIL-SCRIPT-GT]");
            output = output.Replace(Global.Cr, "[MIL-SCRIPT-CR]");
            output = output.Replace(Global.Lf, "[MIL-SCRIPT-LF]");
            return output;
        }


        private string DecodeScript(string script)
        {
            var output = script.Replace("[MIL-SCRIPT-LT]", "<");
            output = output.Replace("[MIL-SCRIPT-GT]", ">");
            output = output.Replace("[MIL-SCRIPT-CR]", Global.Cr);
            output = output.Replace("[MIL-SCRIPT-LF]", Global.Lf);
            return output;
        }

        #endregion
    }
}