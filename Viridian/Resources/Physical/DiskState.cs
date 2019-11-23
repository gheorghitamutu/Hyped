using System;
using System.Globalization;
using System.Xml;
using Viridian.Exceptions;

namespace Viridian.Resources.Physical
{
    public class DiskState
    {
        private DiskState(long fileSize, bool inUse, long? minInternalSize, int physicalSectorSize, int alignment, int? fragmentationPercentage)
        {
            FileSize = fileSize;
            InUse = inUse;
            MinInternalSize = minInternalSize;
            PhysicalSectorSize = physicalSectorSize;
            Alignment2 = alignment;
            FragmentationPercentage = fragmentationPercentage;
        }

        public long GetFileSize => FileSize;
        public bool GetInUse => InUse;
        public long? GetMinInternalSize => MinInternalSize;
        public long GetPhysicalSectorSize => PhysicalSectorSize;
        public long GetAlignment => Alignment;
        public long? GetFragmentationPercentage => FragmentationPercentage;
        private long FileSize { get => FileSize2; set => FileSize2 = value; }
        private long FileSize2 { get => FileSize3; set => FileSize3 = value; }
        private long FileSize3 { get => FileSize4; set => FileSize4 = value; }
        private bool InUse { get => InUse2; set => InUse2 = value; }
        private long FileSize4 { get => FileSize6; set => FileSize6 = value; }
        private bool InUse2 { get; set; }
        private long? MinInternalSize { get; }
        private int Alignment => Alignment2;
        private int PhysicalSectorSize { get; }
        private int Alignment2 { get; set; }
        private int? FragmentationPercentage { get; set; }
        private long FileSize6 { get; set; }

        public static DiskState Parse(string embeddedInstance)
        {
            if (embeddedInstance == null) throw new ViridianException("", new ArgumentNullException(nameof(embeddedInstance)));

            try
            {
                long? minInternalSize;
                int? fragmentationPercentage;

                var doc = new XmlDocument();
                doc.LoadXml(embeddedInstance);

                var nodelist = doc.SelectNodes(@"/INSTANCE/@CLASSNAME");

                if (nodelist == null)
                    return null;

                if (nodelist.Count != 1)
                    throw new ViridianException("", new FormatException());

                if (nodelist[0].Value != "Msvm_VirtualHardDiskState")
                    throw new ViridianException("", new FormatException());

                // FileSize
                nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'FileSize']/VALUE/child::text()");

                if (nodelist == null)
                    return null;

                if (nodelist.Count != 1) 
                    throw new ViridianException("", new FormatException());

                var fileSize = long.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);

                // InUse should be false
                nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'InUse']/VALUE/child::text()");

                if (nodelist == null) 
                    return null;

                if (nodelist.Count != 1) 
                    throw new ViridianException("", new FormatException());

                var inUse = bool.Parse(nodelist[0].Value);

                // MinInternalSize
                nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'MinInternalSize']/VALUE/child::text()");

                if (nodelist == null)
                    return null;

                switch (nodelist.Count)
                {
                    case 0:
                        minInternalSize = null;
                        break;
                    case 1:
                        minInternalSize = long.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);
                        break;
                    default:
                        throw new ViridianException("", new FormatException());
                }

                // PhysicalSectorSize
                nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'PhysicalSectorSize']/VALUE/child::text()");

                if (nodelist == null)
                    return null;

                if (nodelist.Count != 1) 
                    throw new ViridianException("", new FormatException());

                var physicalSectorSize = int.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);

                // Alignment
                nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'Alignment']/VALUE/child::text()");

                if (nodelist == null) return null;

                if (nodelist.Count != 1)
                    throw new ViridianException("", new FormatException());

                var alignment = int.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);

                // FragmentationPercentage
                nodelist = doc.SelectNodes(@"//PROPERTY[@NAME = 'FragmentationPercentage']/VALUE/child::text()");

                if (nodelist == null)
                    return null;

                switch (nodelist.Count)
                {
                    case 0:
                        fragmentationPercentage = null;
                        break;
                    case 1:
                        fragmentationPercentage = int.Parse(nodelist[0].Value, CultureInfo.InvariantCulture);
                        break;
                    default:
                        throw new ViridianException("", new FormatException());
                }

                return new DiskState(fileSize, inUse, minInternalSize, physicalSectorSize, alignment, fragmentationPercentage);

            }
            catch (XmlException ex)
            {
                throw new ViridianException("", new FormatException(null, ex));
            }
            catch (OverflowException ex)
            {
                throw new ViridianException("", new FormatException(null, ex));
            }
        }
    }
}
