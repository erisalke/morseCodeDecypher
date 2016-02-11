using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace morseCodeDecypher
{
    public class Program
    {
        private static IDictionary GetLetterCodes()
        {
            return new Dictionary<string,string>()
                    {
                      { ".-", "a" },
                      { "-...", "b" },
                      { "-.-.", "c" },
                      // "-.."  : "d",
                      // "."    : "e",
                      // "..-." : "f",
                      // "--."  : "g",
                      // "...." : "h",
                      // ".."   : "i",
                      // ".---" : "j",
                      // "-.-"  : "k",
                      // ".-.." : "l",
                      // "--"   : "m",
                      // "-."   : "n",
                      // "---"  : "o",
                      // ".--." : "p",
                      // "--.-" : "q",
                      // ".-."  : "r",
                      // "..."  : "s",
                      // "-"    : "t",
                      // "..-"  : "u",
                      // "...-" : "v",
                      // ".--"  : "w",
                      // "-..-" : "x",
                      // "-.--" : "y",
                      // "--.." : "z"
                    };
        }

        private static IEnumerable ReadDictionaryFromFile(){
          return new List<string>() { "word1", "word2" };
        }

        private static string GetEncryptedMessage(){
            return "-.-";
        }

        private static void JustWriteToConsole(IEnumerable sentences){
          foreach(string x in sentences){
            Console.WriteLine(x);
          }
        }

        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("--------- Morse Code Decypher v.1.0 ---------");

            var message = GetEncryptedMessage();
            var letterCodesHash = GetLetterCodes();
            var dictionary = ReadDictionaryFromFile();

            var decoder = new Decoder( message,
                                       letterCodesHash,
                                       dictionary);

            decoder.Calculate();

            JustWriteToConsole(decoder.ReturnResults());
            //Console.Read();
        }
    }

    public class Decoder
    {
        private string factoryValue;
        private IDictionary letterCodesHash;
        private IEnumerable dictionary;
        private IEnumerable results;

        public Decoder(string encodedMsg,
                       IDictionary letterCodesHash,
                       IEnumerable dictionary)
        {
            this.factoryValue = encodedMsg;
            this.letterCodesHash = letterCodesHash;
            this.dictionary = dictionary;

            this.results = new List<string>(){
                              "not yet ready man",
                              "but getting better"
                            };
        }

        public void Calculate()
        {
// damnstillempty... :)
        }

        // public IList ReturnResults()
        public IEnumerable<string> ReturnResults()
        {
            return (this.results as IEnumerable<string>);
        }
    }
}
