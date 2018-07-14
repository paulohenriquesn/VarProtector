namespace VarProtector
{
    public class MemoryHistoryReader
    {
        public int value { get; set; }
        public bool status { get; set; }
        public int position { get; set; }

    }

    public class varProtector
    {

        private int Counter = 0;
        MemoryHistoryReader reader = new MemoryHistoryReader();

        List<int> valuesProtected = new List<int>();
        List<string> HistoryMemory = new List<string>();
        Dictionary<int, bool> valueToken = new Dictionary<int, bool>(); //value <pos> <status>

        public void memoryWrite(int pos)
        {
            HistoryMemory.Add($"{valuesProtected[pos]} <{pos}> <{valueToken[valuesProtected[pos]]}>");

        }

        MemoryHistoryReader memoryRead(int pos)
        {
            string historyText = HistoryMemory[pos];
            string[] historySplit = historyText.Split();

            MemoryHistoryReader reader = new MemoryHistoryReader()
            {
                value = int.Parse(historySplit[0]),
                status = bool.Parse(historySplit[2].Replace("<", String.Empty).Replace(">", String.Empty).ToLower()),
                position = int.Parse(historySplit[1].Replace("<", String.Empty).Replace(">", String.Empty).ToLower())
            };
            return reader;
        }

        public void memoryProtect(int pos)
        {
            reader = memoryRead(pos);
                if (!reader.status) { if (reader.value != valuesProtected[pos]) { Detected(); } }          
        }

        public void changeValue(int pos, int value)
        {
            try
            {
                reader = memoryRead(pos);

                valueToken[reader.value] = true;
                valueToken.Add(value, true);
                if (valueToken[valuesProtected[pos]])
                {
                    valuesProtected[pos] = value;
                    HistoryMemory[pos] = $"{valuesProtected[pos]} <{pos}> <{valueToken[valuesProtected[pos]]}>";
                    valueToken[value] = false;
                    valueToken.Remove(reader.value);
                }
                else { Detected(); }

            }
            catch { Detected(); }
          
           
        }

        public int getValue(int pos) { return valuesProtected[pos]; }

        public void makeValue(int value)
        {
            valuesProtected.Add(value);
            valueToken.Add(value, false);
            memoryWrite(Counter);
            Counter += 1;
        }

        void Detected()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Beep();
            Console.WriteLine("[VarProtector] Value Changed With Debugger!");
            Console.ResetColor();
        }
    }
}
