using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlankRemover
{
    class TextModifier
    {
        /// <summary>
        /// 여백을 지운다기보다는 <br/>
        /// 불필요한 줄바꿈을 지웁니다.
        /// </summary>
        /// <param name="input">입력 문자열</param>
        /// <returns>출력 문자열</returns>
        static public string RemoveBlank(string input)
        {
            input = makeCorrectNewline(input);
            StringBuilder output = new StringBuilder("");
            int startIndex = 0;
            int count = 0;

            while (true)
            {
                while ((startIndex != input.Length) && (char.IsWhiteSpace(input[startIndex])))
                    startIndex++;
                if (startIndex == input.Length)
                    break;
                count = 1;
                while ((startIndex + count != input.Length) && (input[startIndex + count - 1] != '\n'))
                    count++;
                output.Append(input.Substring(startIndex,count));
                startIndex += count;
            }

            return output.ToString();
        }

        /// <summary>
        /// 잘못된 개행문자를 올바르게 고쳐줍니다.
        /// </summary>
        /// <param name="original">원본 문자열</param>
        /// <returns>수정된 문자열</returns>
        static private string makeCorrectNewline(string original)
        {
            StringBuilder str = new StringBuilder(original);
            for (int i = 0, j = 0; i < original.Length; i++, j++)
            {
                if (original[i] == '\r')
                {
                    if ((i + 1 == original.Length) || (original[i + 1] != '\n'))
                    {
                        str = str.Insert(j + 1, '\n');
                        j++;
                    }
                    else
                    {
                        i++;
                        j++;
                    }
                }
                else if (original[i] == '\n')
                {
                    str = str.Insert(j, '\r');
                    j++;
                }
                else if (original[i] == '\v')
                {
                    str = str.Replace("\v", "\r\n", j, 1);
                    j++;
                }
            }
            return str.ToString();
        }
    }
}
