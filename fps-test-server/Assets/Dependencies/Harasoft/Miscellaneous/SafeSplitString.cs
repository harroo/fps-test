
/*

 Okay so you're probably wondering "Why?" right?
 It all started when I was 13, no I'm kidding.

 Compiling CIL-Images on GNU/Linux and executing
 them on Windows NT Machines causes an exception.
 "Method not found: 'System.String[] System.String.Split(Char ..."
  Blah blah you get the idea.

  Honestly we've no idea why this happens, and it only happens
  on Windows as aforementioned..

  The simplest fix for this is to just write a replacement function
  that operates the same way, and use that instead.

  Now you know.

  - Kiera

*/

using System;
using System.Collections.Generic;

namespace Harasoft {

    public static partial class Misc {

        //windows hates string.Split, so i made my own 1
        //and yea ik its unoptimized, it just need to do tha job
        public static string[] SplitString (string input, char marker) {

            List<string> strings = new List<string>();

            string temp = "";

            foreach (char c in input) {

                if (c == marker) {

                    strings.Add(temp);
                    temp = "";
                    continue;
                }

                temp += c.ToString();
            }

            strings.Add(temp);

            return strings.ToArray();
        }
    }
}
