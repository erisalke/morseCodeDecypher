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
        public static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("--------- Morse Code Decypher v.1.0 ---------");

            var message = Aux.GetEncryptedMessage();
            var letterCodesHash = Aux.GetLetterCodes();
            var dictionary = Aux.ReadDictionaryFromFile();

            var decoder = new Decoder(message, letterCodesHash, dictionary);
            decoder.Decode();
            Aux.JustWriteToConsole("\nPossible results:", decoder.Results);

            var encoder = new Encoder(decoder.Results, letterCodesHash);
            encoder.Encode();
            Aux.JustWriteToConsole("", new List<string>(){"Verify against the original message:", message});
            Aux.JustWriteToConsole("Results translated back to morse:", encoder.Results);
        }
    }


    public class Decoder
    {
        public List<string> Results
        {
           get
           {
              return results;
           }
        }

        public Decoder(string encodedMsg,
                       Dictionary<string,string> letterCodesHash,
                       List<string> dictionary)
        {
            this.message = encodedMsg;
            this.letterCodesHash = letterCodesHash;
            this.dictionary = dictionary;
            this.results = new List<string>() {};
        }

        // Decode method does the decoding
        // accepts head => current position in the message,
        //         word => currently builded word
        //         sentence => solution we have so far
        public void Decode(int head=0, string word="", string sentence="")
        {
          // stop condition (head overreaches the message length)
          if (head < this.message.Length){
            string letter = "";

            // open all possible paths from this point
            for (var offset = 1; offset <= Math.Min(MaxCodeSize, this.message.Length-head); offset++)
            {
              var dotsAndDashes = this.message.Substring(head, offset);
              // try to get the letter from the dotsAndDashes code
              if (this.letterCodesHash.TryGetValue(dotsAndDashes, out letter))
              {
                var newWord = word + letter;
                // find all words matching this beginning
                var possibleWords = this.dictionary.Where(x=>x.StartsWith(newWord));
                if (possibleWords.Count() > 0){
                  // if we have a completed word, add it to solutions and move on
                  if(possibleWords.FirstOrDefault(x=>x == newWord) != null){
                    Console.WriteLine(sentence+newWord);
                    if (head+offset == this.message.Length){
                      this.results.Add(sentence+newWord);
                      Console.WriteLine("   ^^^ gracefully terminated!");
                    }

                    // if a words was found, reset the word (2nd parameter), add it to sentence (3rd param)
                    Decode(head+offset, "", sentence+newWord+" ");
                  }

                  // still move on (this call satisfies the condition when
                  // two words have same beginning like "copy" and "copyright")
                  Decode(head+offset, newWord, sentence);
                }
              }
            }
          }
        }

        private const int MaxCodeSize = 4; // no letter is encoded in more than 4dash/dots
        private string message;
        private Dictionary<string,string> letterCodesHash;
        private List<string> dictionary;
        private List<string> results;
    }

    // MorseEncoder just to be sure the results can be translated back
    public class Encoder
    {
      public List<string> Results
      {
         get
         {
            return results;
         }
      }

      public Encoder(List<string> messages,Dictionary<string,string> letterCodesHash){
        this.messages = messages;
        this.letterCodesHash = letterCodesHash;
        this.results = new List<string>();
      }

      public void Encode(){
        foreach(var m in this.messages){
          var result = "";
          m.Replace(" ", "");
          for (var i=0; i<m.Length; ++i){
            result += this.letterCodesHash.FirstOrDefault(x => x.Value == m[i].ToString()).Key;
          }
          this.results.Add(result);
        }
      }

      private List<string> results;
      private List<string> messages;
      private Dictionary<string,string> letterCodesHash;
    }

    // Some auxiliary static class to keep all the helper methods in one place
    public static class Aux
    {
      public static string GetEncryptedMessage(){
        return ".--..-..-.-.-----.-----....--...-.-.-..-....--.-......----.";
      }

      public static List<string> ReadDictionaryFromFile(){
        return new List<string>(System.IO.File.ReadAllLines(@"./google1000.txt"));
      }

      public static void JustWriteToConsole(string welcomeMsg, IEnumerable sentences){
        Console.WriteLine(welcomeMsg);
        foreach(string x in sentences){
          Console.WriteLine(x);
        }
      }

      public static Dictionary<string,string> GetLetterCodes()
      {
        return new Dictionary<string,string>()
        {
          { ".-", "a" },   { "-...", "b" }, { "-.-.", "c" }, { "-..", "d" },
          { ".", "e" },    { "..-.", "f" }, { "--.", "g" },  { "....", "h" },
          { "..", "i" },   { ".---", "j" }, { "-.-", "k" },  { ".-..", "l" },
          { "--", "m" },   { "-.", "n" },   { "---", "o" },  { ".--.", "p" },
          { "--.-", "q" }, { ".-.", "r" },  { "...", "s" },  { "-", "t" },
          { "..-", "u" },  { "...-", "v" }, { ".--", "w" },  { "-..-", "x" },
          { "-.--", "y" }, { "--..", "z" }
        };
      }
    }
}
