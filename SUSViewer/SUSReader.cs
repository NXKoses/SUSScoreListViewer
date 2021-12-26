using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SUSViewer
{
    internal class SUSReader
    {
        private Regex SusCommandPattern { get; } = new Regex(@"#(?<name>[A-Z]+)\s+\""?(?<value>[^\""]*)", RegexOptions.IgnoreCase);

        private string Title;
        private string ArtistName;
        private string DesignerName;
        private string SongID;
        private string Jacket;
        private int Difficulty;
        private string PlayLevel;
        private string Wave;
        private string WaveOffset;
        public SUSFileData Perse(TextReader reader)
        {
            bool matchAction(Match m, Action<Match> act)
            {
                if (m.Success) act(m);
                return m.Success;
            }

            int linecount = -1;
            while (reader.Peek() >= 0)
            {
                string line = reader.ReadLine();
                linecount++;

                if (matchAction(SusCommandPattern.Match(line), m => ProcessCommand(linecount, m.Groups["name"].Value, m.Groups["value"].Value))) continue;
            }

            return new SUSFileData()
            {
                Difficulty = Difficulty,
                Playlevel = PlayLevel,
                Title = Title,
                Artist = ArtistName,
                Designer = DesignerName,
                Songid = SongID,
                Jacket = Jacket
            };
        }
        private string TrimLiteral(string str)
        {
            return Regex.Match(str, @"(?<=\""?)[^\""]*").Value;
        }
        private void ProcessCommand(int lineIndex, string name, string value)
        {
            switch (name.ToUpper())
            {
                case "TITLE":
                    Title = TrimLiteral(value);
                    break;

                case "ARTIST":
                    ArtistName = TrimLiteral(value);
                    break;

                case "DESIGNER":
                    DesignerName = TrimLiteral(value);
                    break;

                case "DIFFICULTY":
                    Difficulty = int.Parse(value);
                    break;

                case "PLAYLEVEL":
                    PlayLevel = value;
                    break;

                case "SONGID":
                    SongID = TrimLiteral(value);
                    break;

                case "WAVE":
                    Wave = TrimLiteral(value);
                    break;

                case "WAVEOFFSET":
                    WaveOffset = TrimLiteral(value);
                    break;

                case "JACKET":
                    Jacket = TrimLiteral(value);
                    break;

                default:
                    //Debug.WriteLine($"{name}コマンドは処理されません。(行: {lineIndex + 1})");
                    break;
            }
        }
    }
}
