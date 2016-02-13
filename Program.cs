using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

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
                      { "-..", "d" },
                      { ".", "e" },
                      { "..-.", "f" },
                      { "--.", "g" },
                      { "....", "h" },
                      { "..", "i" },
                      { ".---", "j" },
                      { "-.-", "k" },
                      { ".-..", "l" },
                      { "--", "m" },
                      { "-.", "n" },
                      { "---", "o" },
                      { ".--.", "p" },
                      { "--.-", "q" },
                      { ".-.", "r" },
                      { "...", "s" },
                      { "-", "t" },
                      { "..-", "u" },
                      { "...-", "v" },
                      { ".--", "w" },
                      { "-..-", "x" },
                      { "-.--", "y" },
                      { "--..", "z" }
                    };
        }

        private static List<string> ReadDictionaryFromFile(){
          string[] lines = System.IO.File.ReadAllLines(@"./google1000.txt");
          return new List<string>(lines);
        }

        private static string GetEncryptedMessage(){
            //var str = ".--..-..-.-.-----.-----....--...-.-.-..-....--.-......----."; // orgiginal?
            var str = ".--..-..-.-.-----.-----....--...-.-.-..-....--.-......----."; // "SOS HELP" & ?
            return str;
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
            var letterCodesHash = GetLetterCodes() as Dictionary<string,string>;
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
        private Dictionary<string,string> letterCodesHash;
        private List<string> dictionary;
        private List<string> results;

        public Decoder(string encodedMsg,
                       IDictionary letterCodesHash,
                       List<string> dictionary)
        {
            this.factoryValue = encodedMsg;
            this.letterCodesHash = (letterCodesHash as Dictionary<string,string>);
            this.dictionary = dictionary;

            this.results = new List<string>(){
                              "not yet ready man",
                              "but getting better"
                            };
        }

        // private IEnumerable GetPossibilities(string word, ){
        //   var newChar =
        //   return this.dictionary.Where(x => x.containsOrStartsWith(word+newChar));
        // }

        public void Calculate(int head=0, string word="", string sentence="")
        {
          if (head == this.factoryValue.Length -1){
            //all makes sense check?
              // Add solution to List of solutions
              this.results.Add(sentence);
          }
          else{
            string value = "";
            for (var offset = 1; offset<Math.Min(5, this.factoryValue.Length-head); offset++){
              string ch = this.factoryValue.Substring(head, offset);
              if (this.letterCodesHash.TryGetValue(ch, out value))
              {
                var possibleWords = this.dictionary.Where(x=>x.StartsWith(word+value));
                if (possibleWords.Count() > 0){
                  var dup = possibleWords.FirstOrDefault(x=>x == word+value);
                  if(dup != null){
                    Console.WriteLine("Head {0}, word: {1}", head, word+value);
                    Calculate(head+offset, "", sentence+word+value+" ");
                  }

                  Calculate(head+offset, word+value, sentence);
                }
              }

              // Calculate(head+offset, word+"a", sentence+word);
            }
          }
        }



        // public IList ReturnResults()
        public IEnumerable<string> ReturnResults()
        {
            return (this.results as IEnumerable<string>);
        }
    }
}
