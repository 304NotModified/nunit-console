// ***********************************************************************
// Copyright (c) 2009 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;

namespace NUnit.Framework.Constraints.Tests
{
    /// <summary>
    /// Summary description for PathConstraintTests.
    /// </summary>]
    [TestFixture]
    public class SamePathTest_Windows : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathConstraint( @"C:\folder1\file.tmp" ).IgnoreCase;
            expectedDescription = @"Path matching ""C:\folder1\file.tmp""";
            stringRepresentation = "<samepath \"C:\\folder1\\file.tmp\" ignorecase>";
        }

        object[] SuccessData = new object[] 
            { 
                @"C:\folder1\file.tmp", 
                @"C:\Folder1\File.TMP",
                @"C:\folder1\.\file.tmp",
                @"C:\folder1\folder2\..\file.tmp",
                @"C:\FOLDER1\.\folder2\..\File.TMP",
                @"C:/folder1/file.tmp"
            };
        object[] FailureData = new object[] 
            { 
                123,
                @"C:\folder2\file.tmp",
                @"C:\folder1\.\folder2\..\file.temp"
            };
        string[] ActualValues = new string[] 
            { 
                "123",
                "\"C:\\folder2\\file.tmp\"",
                "\"C:\\folder1\\.\\folder2\\..\\file.temp\""
            };
    }

    [TestFixture]
    public class SamePathTest_Linux : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathConstraint(@"/folder1/file.tmp").RespectCase;
            expectedDescription = @"Path matching ""/folder1/file.tmp""";
            stringRepresentation = @"<samepath ""/folder1/file.tmp"" respectcase>";
        }

        object[] SuccessData = new object[] 
            { 
                @"/folder1/file.tmp", 
                @"/folder1/./file.tmp",
                @"/folder1/folder2/../file.tmp",
                @"/folder1/./folder2/../file.tmp",
                @"\folder1\file.tmp"
            };
        object[] FailureData = new object[] 
            { 
                123,
                @"/folder2/file.tmp",
                @"/folder1/./folder2/../file.temp",
                @"/Folder1/File.TMP",
                @"/FOLDER1/./folder2/../File.TMP",
            };
        string[] ActualValues = new string[] 
            { 
                "123",
                "\"/folder2/file.tmp\"",
                "\"/folder1/./folder2/../file.temp\"",
                "\"/Folder1/File.TMP\"",
                "\"/FOLDER1/./folder2/../File.TMP\"",
            };
    }

    [TestFixture]
    public class SamePathOrUnderTest_Windows : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathOrUnderConstraint( @"C:\folder1\folder2" ).IgnoreCase;
            expectedDescription = @"Path under or matching ""C:\folder1\folder2""";
            stringRepresentation = @"<samepathorunder ""C:\folder1\folder2"" ignorecase>";
        }

        object[] SuccessData = new object[]
            {
                @"C:\folder1\folder2",
                @"C:\Folder1\Folder2",
                @"C:\folder1\.\folder2",
                @"C:\folder1\junk\..\folder2",
                @"C:\FOLDER1\.\junk\..\Folder2",
                @"C:/folder1/folder2",
                @"C:\folder1\folder2\folder3",
                @"C:\folder1\.\folder2\folder3",
                @"C:\folder1\junk\..\folder2\folder3",
                @"C:\FOLDER1\.\junk\..\Folder2\temp\..\Folder3",
                @"C:/folder1/folder2/folder3",
            };
        object[] FailureData = new object[]
            {
                123,
                @"C:\folder1\folder3",
                @"C:\folder1\.\folder2\..\file.temp"
            };
        string[] ActualValues = new string[]
            {
                "123",
                "\"C:\\folder1\\folder3\"",
                "\"C:\\folder1\\.\\folder2\\..\\file.temp\""
            };
    }

    [TestFixture]
    public class SamePathOrUnderTest_Linux : ConstraintTestBase
    {
        [SetUp]
        public void SetUp()
        {
            theConstraint = new SamePathOrUnderConstraint( @"/folder1/folder2"  ).RespectCase;
            expectedDescription = @"Path under or matching ""/folder1/folder2""";
            stringRepresentation = @"<samepathorunder ""/folder1/folder2"" respectcase>";
        }

        object[] SuccessData = new object[]
            {
                @"/folder1/folder2",
                @"/folder1/./folder2",
                @"/folder1/junk/../folder2",
                @"\folder1\folder2",
                @"/folder1/folder2/folder3",
                @"/folder1/./folder2/folder3",
                @"/folder1/junk/../folder2/folder3",
                @"\folder1\folder2\folder3",
            };
        object[] FailureData = new object[]
            {
                123,
                "/Folder1/Folder2",
                "/FOLDER1/./junk/../Folder2",
                "/FOLDER1/./junk/../Folder2/temp/../Folder3",
                "/folder1/folder3",
                "/folder1/./folder2/../folder3",
				"/folder1"
            };
        string[] ActualValues = new string[]
            {
                "123",
                "\"/Folder1/Folder2\"",
                "\"/FOLDER1/./junk/../Folder2\"",
                "\"/FOLDER1/./junk/../Folder2/temp/../Folder3\"",
                "\"/folder1/folder3\"",
                "\"/folder1/./folder2/../folder3\"",
				"\"/folder1\""
            };
    }
}