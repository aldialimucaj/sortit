﻿using Sortit.al.aldi.sortit.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sortit.al.aldi.sortit.control
{
    class SortFilesAlpha : SortImpl
    {
        public static readonly string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        public const string rest = "_REST";

        private int _depth = 1;

        private String _destination;

        public String Destination
        {
            get { return _destination; }
            set { _destination = value.EndsWith("\\") ? value : value + "\\"; }
        }

        public SortFilesAlpha(String dest, int depth) : base(false)
        {
            Destination = dest;
            _depth = depth;
        }

        public SortFilesAlpha(String dest, int depth, bool copy)
            : base(copy)
        {
            Destination = dest;
            _depth = depth;
        }

        public SortFilesAlpha(String dest, int depth, bool copy, bool overwrite)
            : base(copy, overwrite)
        {
            Destination = dest;
            _depth = depth;
        }

        /// <summary>
        /// Sorting Function for alphabetically sorting algorithm.
        /// This algorithm would sort the files depending or the recursion depth specified.
        /// If for example the depth is set to 0 then it will sort files with the first level or
        /// recursion which is just by the first letter of the filename. 
        /// Example. Testfile.pdf will be moved to .\T\Testfile.pdf
        /// Recursion level of 1 will move it to .\T\TE\Testfile.pdf
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public override String RenameFunc(File2Sort file)
        {

            String returnPath = Destination;

            int recursion = 0;

            while (recursion < _depth )
            {
                if (file.FileName.Length  > recursion)
                {
                    returnPath = returnPath.EndsWith("\\") ? returnPath : returnPath + "\\";

                    if (alphabet.Contains(file.FileName.ToUpper().Substring(recursion, 1)))
                    {
                        returnPath += file.FileName.Substring(0, recursion + 1).ToUpper();
                    }
                    else
                    {
                        returnPath += "_REST";
                        break;
                    }
                }
                recursion++;
            }
            returnPath = returnPath.EndsWith("\\") ? returnPath : returnPath + "\\";
            returnPath += file.FileName;

            return returnPath;
        }
    }
}
