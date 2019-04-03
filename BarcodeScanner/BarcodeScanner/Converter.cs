using System;
using System.Collections.Generic;
using System.Text;


    /// <summary>
    /// Class to do the conversion
    /// </summary>
    public class Converter
    {
    #region "Enums"
        /// <summary>
        /// Special cases
        /// </summary>
        public enum SpecialTypes
        {
            None = 0,
            UnicodeAsDecimal = 1
        }
    #endregion

    #region "Public Methods"
        /// <summary>
        /// Convert a file from source codepage to destination codepage
        /// </summary>
        /// <param name="filename">filename</param>
        /// <param name="destDir">output directory</param>
        /// <param name="sourceCP">source codepage</param>
        /// <param name="destCP">destination codepage</param>
        /// <param name="specialType">SpecialTypes</param>
        /// <param name="outputMetaTag">True=output meta tag</param>
        /// <returns></returns>
        public static bool ConvertFile(string filename, string destDir, int sourceCP,int  destCP, SpecialTypes specialType, bool outputMetaTag)
        {
            //source file data
            string fileData = "";
            //get the encodings
            Encoding sourceEnc = Encoding.GetEncoding(sourceCP);
            Encoding destEnc = Encoding.GetEncoding(destCP);
            System.IO.FileStream fw = null;

            //get the output filename
            string outputFilename = filename.Substring(filename.LastIndexOf("\\") + 1);
            //check if the file exists
            if (!System.IO.File.Exists(filename))
            {
                throw new System.IO.FileNotFoundException(filename);
            }

            try
            {
                //check or create the output directory
                if (!System.IO.Directory.Exists(destDir))
                {
                    System.IO.Directory.CreateDirectory(destDir);
                }
                //check if we need to output meta tags
                if (outputMetaTag)
                {
                    fileData = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset="
                                + destEnc.WebName + "\" />";
                }
                //check we've got a backslash at the end of the pathname
                if (destDir.EndsWith("\\") == false)
                    destDir += "\\";

                //read in the source file
                fileData += System.IO.File.ReadAllText(filename);
                //check for any special cases
                switch (specialType)
                {
                    case SpecialTypes.UnicodeAsDecimal:
                        fileData = ConvertDecimals(fileData);
                        break;
                    case SpecialTypes.None:
                        //do nothing
                        break;
                }
                //put the data into an array
                byte[] bSource = sourceEnc.GetBytes(fileData);
                //do the conversion
                byte[] bDest = System.Text.Encoding.Convert(sourceEnc, destEnc, bSource);
                //write out the file
                fw = new System.IO.FileStream(destDir + outputFilename, System.IO.FileMode.Create);
                //02/05/2007 need to write first to bytes when saving as unicode
                if (destEnc.CodePage == 1200)
                {
                    fw.WriteByte(0xFF);
                    fw.WriteByte(0xFE);
                }
                fw.Write(bDest, 0, bDest.Length);
                return true;
            }
            catch (Exception ex)
            {
                //just throw the exception back up
                throw ex;
            }
            finally
            {
                //clean up the stream
                if (fw != null)
                {
                    fw.Close();
                }
                fw.Dispose();
            }
        }
    #endregion

    #region "Private Methods"
        /// <summary>
        /// Do some special processing for special number encoded data
        /// </summary>
        /// <param name="sData">original filedata</param>
        /// <returns></returns>
        private static string ConvertDecimals(string sData)
        {
            //search for &# + at least 2 digits + ;
            //27/03/2007 Changed the regular expression to look for 2 or more digits
            //previously it was only looking for 4 digit groups
            System.Text.RegularExpressions.Regex re  = new System.Text.RegularExpressions.Regex("&#(\\d){2,};");
            System.Text.RegularExpressions.Match match;
            int iVal = 0;
            StringBuilder sReturnData = new StringBuilder();

            while (sData.Length > 0)
            {
                match = re.Match(sData);
                if (match.Length == 0)
                {
                    //no match so just add the rest of the data
                    sReturnData.Append(sData);
                    sData = "";
                }
                else
                {
                    //we got a match so put the first bit into the return string
                    sReturnData.Append(sData.Substring(0, match.Index));
                    //get rid of the bit we already searc5hed
                    sData = sData.Substring(match.Index + match.Length);
                    //get the char val
                    //27/03/2007 length parameter in substring is not fixed any more
                    //but it will always be the length of the match - 3 (ie &#;)
                    iVal = int.Parse(match.Value.Substring(2, match.Length-3));
                    //output it
                    sReturnData.Append(char.ConvertFromUtf32(iVal));
                }
            }
            return sReturnData.ToString();
        }
    #endregion
    }
