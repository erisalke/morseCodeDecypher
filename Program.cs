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
        private static Dictionary<string,string> GetLetterCodes()
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
            var str = ".--..-..-.-.-----.-----....--...-.-.-..-....--.-......----.";
                      //  .--..-..-.-.-----.-----....--...-.-.-..-....--.-......----.
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

            var decoder = new Decoder(message, letterCodesHash, dictionary);
            decoder.Calculate();
            JustWriteToConsole(decoder.ReturnResults());

            var encoder = new Encoder((List<string>)decoder.ReturnResults(), letterCodesHash);
            encoder.Encode();
            JustWriteToConsole(encoder.ReturnResults());
        }
    }

    public class Encoder
    {
      private List<string> results;
      private List<string> messages;
      private Dictionary<string,string> letterCodesHash;

      public Encoder(List<string> messages,Dictionary<string,string> letterCodesHash){
        this.messages = messages;
        this.letterCodesHash = letterCodesHash;
        this.results = new List<string>();
      }

      public void Encode(){
        foreach(var m in this.messages){
          var result ="";
          m.Replace(" ", "");
          for (var i=0; i<m.Length; ++i){
            result += this.letterCodesHash.FirstOrDefault(x => x.Value == m[i].ToString()).Key;
          }
          this.results.Add(result);
        }
      }

      public IEnumerable<string> ReturnResults()
      {
          return (this.results as IEnumerable<string>);
      }
    }

    public class Decoder
    {
        private string factoryValue;
        private Dictionary<string,string> letterCodesHash;
        private List<string> dictionary;
        private List<string> results;

        public Decoder(string encodedMsg,
                       Dictionary<string,string> letterCodesHash,
                       List<string> dictionary)
        {
            this.factoryValue = encodedMsg;
            this.letterCodesHash = letterCodesHash;
            this.dictionary = dictionary;
            this.results = new List<string>();
        }

        public void Calculate(int head=0, string word="", string sentence="")
        {
          if (head <= this.factoryValue.Length){
            string value = "";
            var gggg = Math.Min(4, this.factoryValue.Length-head);


if (head+1 == this.factoryValue.Length){
  Console.WriteLine("H{0}, {1}, {2}", head, this.factoryValue.Length, this.factoryValue.Substring(head, 1));
}
            for (var offset = 1; offset<=gggg; offset++){
              var ch = this.factoryValue.Substring(head, offset);
              if (this.letterCodesHash.TryGetValue(ch, out value))
              {
                var possibleWords = this.dictionary.Where(x=>x.StartsWith(word+value));
                if (possibleWords.Count() > 0){
                  var dup = possibleWords.FirstOrDefault(x=>x == word+value);
                  if(dup != null){
                    Console.WriteLine("{0}, {1}",head+offset,this.factoryValue.Length);
                    if (head+offset == this.factoryValue.Length){
                      this.results.Add(sentence+word+value);
                    Console.WriteLine("Head {0}, word: {1}", head, sentence+word+value+" "+"***");
                    }

                    Console.WriteLine("Head {0}, word: {1}", head, sentence+word+value+" ");
                    Calculate(head+offset, "", sentence+word+value+" ");
                  }

                  Calculate(head+offset, word+value, sentence);
                }
              }
            }
          }
        }

        public IEnumerable<string> ReturnResults()
        {
            return (this.results as IEnumerable<string>);
        }
    }
}
