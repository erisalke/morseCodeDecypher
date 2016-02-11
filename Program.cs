using System;

namespace morseCodeDecypher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("--------- Morse Code Decypher v.1.0 ---------");

            ReadDataFromFile();
            DoJob();

            //Console.Read();
        }

        private static
        public static void DoJob(){

        }
    }

    public class Decoder
    {
        private string factoryValue;

        public Decoder(string encodedMsg,
                       IDictionary<string, string> letterCodesHash,
                       IEnumebable<string> dictionary)
        {
            this.factoryValue = encodedMsg;
            this.letterCodesHash = letterCodesHash;
            this.dictionary = dictionary;
        }

        public void Calculate(){
            
        }

        public string ReturnResult(){
          return "not yet ready man";
        }
    }
}
