using Xamarin.Forms;
using static Common.Enums;

namespace Helpers.UI
{
    public static class TextExtensions
    {
        public static Color GetTextColor(WordType type)
        {
            Color color;
            switch (type)
            {
                case WordType.Adjective:
                    color = Color.Purple;
                    break;
                case WordType.Verb:
                    color = Color.Green;
                    break;
                case WordType.Adverb:
                    color = Color.DarkBlue;
                    break;
                case WordType.Noun:
                    color = Color.DarkOrange;
                    break;
                default:
                    color = Color.Black;
                    break;
            }
            return color;
        }
    }
}
