using FSSystemInfo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FSSystemInfo.Tests
{
    [TestClass()]
    public class FSDiskTests
    {
        [TestMethod()]
        public void DiskInfo()
        {
            SystemInfo sysInfo = new SystemInfo();
            List<string> phiscalDisk = sysInfo.GetPhysicalDisks();

            FSDisk.DiskInfo diskInfo = new FSDisk.DiskInfo();

            string sSerial = "";
            string sModel = "";
            string sFirmware = "";

            bool result = diskInfo.GetHDData(phiscalDisk[0], ref sSerial, ref sModel, ref sFirmware);

            Assert.IsFalse(result);

            if (result)
            {
                Assert.AreEqual(sSerial, "");
                Assert.AreEqual(sModel, "");
                Assert.AreEqual(sFirmware, "");
            }
        }

        [TestMethod()]
        public void DiskHDInfo()
        {
            SystemInfo si = new SystemInfo();

            string result = si.GetHdInfo();

            Assert.AreNotEqual(result, "");
        }
    }
}
