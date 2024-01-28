using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Diagnostics;

using System;
using Mapping;





namespace MappingToStringTestStep00
{
    [TestClass]
    public class UnitTestToS
    {
        [TestMethod]
        [DataRow (new byte[] { 0x32, 0x31, 0x30, 0x30, 0x33, 0x39 })]
        public void TestMethod_CDDFile_SS_Identifier(byte[] inputMsgBytes)
        {


            Assert.AreEqual($"\n<210309>\n", 
                $"\n<{Mapping.ToString.CDDFILE_System_Supplier_Identifier(inputMsgBytes)}>\n"
                );

            Assert.AreEqual(inputMsgBytes.Length, 6);

        }

        [TestMethod]
        [DataRow(new byte[] { 0x02, 0x66, 0x08, 0x09, 0x47, 0x59, 0x20, 0x20, 0x43, 0x66, 0x08, 0x09, 0x47, 0x60, 0x20, 0x20, 0x43 })]
        public void TestMethod_CDDFile_ECU_SW_PN(byte[] inputMsgBytes)
        {


            Assert.AreEqual($"\n< 0x02 0x66 0x08 0x09 0x47 0x59 0x20 0x20 0x43 0x66 0x08 0x09 0x47 0x60 0x20 0x20 0x43 >\n",
                $"\n<{Mapping.ToString.CDDFILE_ECU_Software_Part_Number_Geely(inputMsgBytes)}>\n"
                );

            Assert.AreEqual(inputMsgBytes.Length, 18);

        }

        [TestMethod]
        [DataRow(new byte[] { 0x66, 0x08, 0x09, 0x50, 0x80, 0x20, 0x20, 0x41 })]
        public void TestMethod_CDDFile_ECU_DEL_ASSEMB_PN(byte[] inputMsgBytes)
        {


            Assert.AreEqual($"\n< 0x66 0x08 0x09 0x50 0x80 0x20 0x20 0x41 >\n",
                $"\n<{Mapping.ToString.CDDFILE_ECU_Delivery_Assembly_Part_Number_Geely(inputMsgBytes)}>\n"
                );

            Assert.AreEqual(inputMsgBytes.Length, 8);

        }

    }
}


namespace MappingToStringTestStep01
{
    [TestClass]
    public class UnitTestToS1
    {
        [TestMethod]
        [DataRow(new byte[] { 0x49, 0x50, 0x4D, 0x5f, 0x5f, 0x50, 0x50, 0x5f, 0x56, 0x34, 0x2e, 0x35 })]
        public void TestMethod_CDDFile_Bootloader_SW_VER(byte[] inputMsgBytes)
        {


            /**Assert.AreEqual($"\n<IPM__PP_V2.7>\n",
                $"\n<{Mapping.ToString.CDDFILE_Supplier_Bootloader_Software_Version(inputMsgBytes)}>\n"
                );**/

            Assert.AreEqual(inputMsgBytes.Length, 12);

        }

        [TestMethod]
        [DataRow(new byte[] { 0x31, 0x37, 0x00, 0x47 })]
        public void TestMethod_CDDFile_ECU_SERIAL_NO(byte[] inputMsgBytes)
        {


            Assert.AreEqual($"\n< 0x31, 0x37, 0x00, 0x47 >\n",
                $"\n<{Mapping.ToString.CDDFILE_ECU_Serial_Number(inputMsgBytes)}>\n"
                );

            Assert.AreEqual(inputMsgBytes.Length, 8);

        }

        [TestMethod]
        [DataRow(new byte[] { 0x4C, 0x2D, 0x52, 0x30, 0x34, 0X45, 0x30, 0x31, 0x32, 0x33, 0x30, 0x33, 0x32, 0x32, 0x5f, 0x31 })]  
        public void TestMethod_CDDFile_SUPPLIER_SW_VER(byte[] inputMsgBytes)
        {


            Assert.AreEqual($"\n< L-R04E01230322_1 >\n",   // jiu ban cdd 文件 16 字节
                $"\n<{Mapping.ToString.CDDFILE_Supplier_Software_Version(inputMsgBytes)}>\n"
                );

            Assert.AreEqual(inputMsgBytes.Length, 16);

        }

    }
}






namespace MappingToStringTestStep02
{
    [TestClass]
    public class UnitTestToS2
    {
        [TestMethod]
        [DataRow(new byte[] { 0x4C, 0x2D, 0x52, 0x30, 0x34, 0X45, 0x30, 0x31, 0x32, 0x33, 0x30, 0x33, 0x32, 0x32, 0x5f, 0x31 })]
        public void TestMethod_CDDFile_SW_VER_CAM_APP(byte[] inputMsgBytesVer)
        {


            Assert.AreEqual($"\n<210309>\n",
                $"\n<{Mapping.ToString.CDDFILE_Supplier_Software_Version_for_CAM_APP(inputMsgBytesVer)}>\n"
                );

            Assert.AreEqual(inputMsgBytesVer.Length, 16);

        }

        [TestMethod]
        [DataRow(new byte[] { 0x4C, 0x2D, 0x52, 0x30, 0x34, 0X45, 0x37, 0x33, 0x32, 0x33, 0x30, 0x30, 0x32, 0x31, 0x5f, 0x31 })]
        public void TestMethod_CDDFile_SW_VER_FUSION_APP(byte[] inputMsgBytesVer)
        {


            Assert.AreEqual($"\n<210309>\n",
                $"\n<{Mapping.ToString.CDDFILE_Supplier_Software_Version_for_Fusion_APP(inputMsgBytesVer)}>\n"
                );

            Assert.AreEqual(inputMsgBytesVer.Length, 16);

        }

        [TestMethod]
        [DataRow(new byte[] { 0x4C, 0x2D, 0x52, 0x30, 0x34, 0X45, 0x30, 0x31, 0x32, 0x33, 0x30, 0x33, 0x32, 0x32, 0x5f, 0x31 })]
        public void TestMethod_CDDFile_SW_VER_USS_APP(byte[] inputMsgBytesVer)
        {


            Assert.AreEqual($"\n<WAMCC R_PP_V1.03>\n",
                $"\n<{Mapping.ToString.CDDFILE_Supplier_Software_Version_for_USS_APP(inputMsgBytesVer)}>\n"
                );

            Assert.AreEqual(inputMsgBytesVer.Length, 16);

        }

    }
}



namespace MappingToStringTestStep03
{
    [TestClass]
    public class UnitTestToS3
    {
        [TestMethod]
        [DataRow(new byte[] { /*0x20,*/ 0x45, 0x53, 0x35, 0x4E, 0x33, 0x51, 0x30, 0x30, 0x31, 0x42 })]
        public void TestMethod_CDDFile_PCB_SERIAL_NO(byte[] inputMsgBytesOther)
        {


            Assert.AreEqual($"\n< ES5N3Q001B >\n",
                $"\n<{Mapping.ToString.CDDFILE_PCB_Serial_Number(inputMsgBytesOther)}>\n"
                );

            Assert.AreEqual(inputMsgBytesOther.Length, 13);

        }


        [TestMethod]
        [DataRow(new byte[] { /*0x20,*/ 0x45, 0x53, 0x35, 0x4E, 0x33, 0x51, 0x30, 0x30, 0x31, 0x42, 0x40, 0x40 })]
        public void TestMethod_CDDFile_SYSTEM_Heater_Cur(byte[] inputMsgBytesOther)
        {


            Assert.AreEqual($"\n< ES5N3Q001B >\n",
                $"\n<{Mapping.ToString.CDDFILE_Heater_Current(inputMsgBytesOther)}>\n"
                );

            Assert.AreEqual(inputMsgBytesOther.Length, 15);

        }


        [TestMethod]
        [DataRow(new byte[] { /*0x20,*/ 0x32, 0x30, 0x33, 0x33, 0x30, 0x33, 0x36, 0x31, 0x31, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, 0x40, })]
        public void TestMethod_CDDFile_ECU_Accurate_TraCode(byte[] inputMsgBytesOther)
        {


            Assert.AreEqual($"\n<202303261>\n",
                $"\n<{Mapping.ToString.CDDFILE_ECU_Component_accurate_tracing_code(inputMsgBytesOther)}>\n"
                );

            Assert.AreEqual(inputMsgBytesOther.Length, 155);

        }


    }

}







namespace UnitTestFrameJan28
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
