using System.Xml.Serialization;

namespace LabelPrinter
{
    [XmlRootAttribute("LabelPrinter")]
    public class Config
    {
        public Config(string nText, int nNummernkreis, int nAnfang, int nEnde, int nOffsetX, int nOffsetY, float nEtikettenBreite, float nEtikettenHoehe, float nRandLinks,
            float nRandOben, string nFontCompanyName, int nFontCompanySize, string nFontSerialName, int nFontSerialSize)
        {
            Text = nText;
            Nummernkreis = nNummernkreis;
            Anfang = nAnfang;
            Ende = nEnde;
            OffsetX = nOffsetX;
            OffsetY = nOffsetY;
            EtikettenBreite = nEtikettenBreite;
            EtikettenHoehe = nEtikettenHoehe;
            RandLinks = nRandLinks;
            RandOben = nRandOben;
            FontCompanyName = nFontCompanyName;
            FontCompanySize = nFontCompanySize;
            FontSerialName = nFontSerialName;
            FontSerialSize = nFontSerialSize;
        }

        public Config() { }

        private string _Text;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private int _Nummernkreis;
        public int Nummernkreis
        {
            get { return _Nummernkreis; }
            set { _Nummernkreis = value; }
        }

        private int _Anfang;
        public int Anfang
        {
            get { return _Anfang; }
            set { _Anfang = value; }
        }

        private int _Ende;
        public int Ende
        {
            get { return _Ende; }
            set { _Ende = value; }
        }

        private int _OffsetX;
        public int OffsetX
        {
            get { return _OffsetX; }
            set { _OffsetX = value; }
        }

        private int _OffsetY;
        public int OffsetY
        {
            get { return _OffsetY; }
            set { _OffsetY = value; }
        }

        private float _EtikettenBreite;
        public float EtikettenBreite
        {
            get { return _EtikettenBreite; }
            set { _EtikettenBreite = value; }
        }

        private float _EtikettenHoehe;
        public float EtikettenHoehe
        {
            get { return _EtikettenHoehe; }
            set { _EtikettenHoehe = value; }
        }

        private float _RandLinks;
        public float RandLinks
        {
            get { return _RandLinks; }
            set { _RandLinks = value; }
        }

        private float _RandOben;
        public float RandOben
        {
            get { return _RandOben; }
            set { _RandOben = value; }
        }

        private string _FontCompanyName;
        public string FontCompanyName
        {
            get { return _FontCompanyName; }
            set { _FontCompanyName = value; }
        }

        private int _FontCompanySize;
        public int FontCompanySize
        {
            get { return _FontCompanySize; }
            set { _FontCompanySize = value; }
        }

        private string _FontSerialName;
        public string FontSerialName
        {
            get { return _FontSerialName; }
            set { _FontSerialName = value; }
        }

        private int _FontSerialSize;
        public int FontSerialSize
        {
            get { return _FontSerialSize; }
            set { _FontSerialSize = value; }
        }
    }
}
