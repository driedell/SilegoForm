namespace GreenPAK_Library
{
    public class PAK
    {
        public string base_die;
        public string package;
        public byte PAK_family;
        public string package_size;
        public string package_weight;
        public int pattern_id_address;

        public bool dual_supply_PAK = false;
        public string dual_supply_vdd_pins;
        public string dual_supply_vdd2_pins;

        public bool power_PAK = false;
        public bool LDO_PAK = false;

        public mTM vdd;
        public mTM vdd2;
        public mTM temp;

        public struct mTM           // Min Typ Max
        {
            public string min;
            public string typ;
            public string max;
        };

        public struct IN
        {
            public string woSchmitt;
            public string wSchmitt;
            public string LVDI;
        };

        public struct OH
        {
            public mTM PP1x;
            public mTM PP2x;
            public mTM ODP1x;
            public mTM ODP2x;
        };

        public struct OL
        {
            public mTM PP1x;
            public mTM PP2x;
            public mTM ODN1x;
            public mTM ODN2x;
            public mTM ODN4x;
        };

        public PIN[] pin;

        public struct PIN
        {
            public int RV;          // Resistor Value
            public int UD;          // Pullup/Pulldown
            public int IM;          // Input mode for OE pins
            public int OM;          // Output mode for OE pins
            public int IO;          // GPIO I/O mode
            public int DR;          // 1x/2x driver selection for GPIO
            public int SD;          // Super Drive selection
            public string PT;       // Pin Type (GPI/GPIO/GPIO_OE/SD/SD_OE/I2C)
            public int OE;          // Output enable
            public byte VS;         // VDD source for Dual Supply

            public string name;
            public string type;
            public string resistor;
            public string description;
        };

        public CNT[] cnt;

        public struct CNT
        {
            public int CK;          // Counter clock select
            public int DA;          // Counter Data
            public byte LN;         // Length (number of bits)
            public int MD;          // Counter mode (count/delay/etc)
            public int SL;          // Selected (counter vs LUT)
            public byte CS;         // Clock Source length (4 bits vs 3 bits)

            public bool used;
            public string mode;
            public string mode_alt;
            public string data_value;
            public mTM time;
            public string timeSI;
        };

        public ACMP[] acmp;

        public struct ACMP
        {
            public int TH;          // Threshold selection
            public int HY;          // Hysteresis selection
            public int GN;          // Gain selection
            public int LB;          // Low Bandwidth Enable

            public bool used;
            public string acmpVIH;
            public string acmpVIL;
            public string low_bw;
            public string hysteresis;
        };

        // OSC stuff

        public int LF_osc_freq;
        public int LF_osc_pre_div;
        public int RC_osc_freq;
        public int RC_osc_freq_alt;
        public int RC_osc_src;
        public int RC_osc_pre_div;
        public int RING_osc_freq;
        public int RING_osc_pre_div;

        // PAK5 stuff

        public int lock_read = 1832;
        public int lock_write_0 = 1871;
        public int lock_write_1 = 1870;
        public int PAK5_I2C_Slave_Address = 1864;

        // PAK4 stuff
        public int lock_status;

        // PAK3 stuff
        public string TM_note;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46531
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46531()
        {
            PAKs.SLG46531 = new PAK();
            PAKs.SLG46531.base_die = "SLG46531";
            PAKs.SLG46531.package = "STQFN-20L";
            PAKs.SLG46531.package_size = "2mm x 3mm";
            PAKs.SLG46531.PAK_family = 5;
            PAKs.SLG46531.package_weight = "0.0090 g";
            PAKs.SLG46531.pattern_id_address = 1840;
            PAKs.SLG46531.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 07
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 08
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 09
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                               },    // 11
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 1  },    // 13
                new PIN() { PT = "GPIO_OE", RV = 1114, UD = 1113, IM = 1116, OM = 1118, OE = 0328,            VS = 1  },    // 14
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 1  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 1  },    // 16
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 1  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 1  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 1  },    // 19
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 1  },    // 20
            };
            PAKs.SLG46531.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46531.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46531.RC_osc_freq = 25000;
            PAKs.SLG46531.RC_osc_freq_alt = 2000000;
            PAKs.SLG46531.RC_osc_src = 1342;
            PAKs.SLG46531.RC_osc_pre_div = 1339;
            PAKs.SLG46531.RING_osc_freq = 25000000;
            PAKs.SLG46531.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46532
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46532()
        {
            PAKs.SLG46532 = new PAK();
            PAKs.SLG46532.base_die = "SLG46532";
            PAKs.SLG46532.package = "STQFN-20L";
            PAKs.SLG46532.package_size = "2mm x 3mm";
            PAKs.SLG46532.PAK_family = 5;
            PAKs.SLG46532.package_weight = "0.0090 g";
            PAKs.SLG46532.pattern_id_address = 1840;
            PAKs.SLG46532.dual_supply_PAK = true;
            PAKs.SLG46532.dual_supply_vdd_pins = "PIN2-PIN10";
            PAKs.SLG46532.dual_supply_vdd2_pins = "PIN12-PIN20";
            PAKs.SLG46532.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 07
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 08
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 09
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                               },    // 11
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 2  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 2  },    // 13
                new PIN() { PT = "VDD2",                                                                              },    // 14
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 2  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 2  },    // 16
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 2  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 2  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 2  },    // 19
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 2  },    // 20
            };
            PAKs.SLG46532.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46532.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46532.RC_osc_freq = 25000;
            PAKs.SLG46532.RC_osc_freq_alt = 2000000;
            PAKs.SLG46532.RC_osc_src = 1342;
            PAKs.SLG46532.RC_osc_pre_div = 1339;
            PAKs.SLG46532.RING_osc_freq = 25000000;
            PAKs.SLG46532.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46533
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46533()
        {
            PAKs.SLG46533 = new PAK();
            PAKs.SLG46533.base_die = "SLG46533";
            PAKs.SLG46533.package = "STQFN-20L";
            PAKs.SLG46533.package_size = "2mm x 3mm";
            PAKs.SLG46533.PAK_family = 5;
            PAKs.SLG46533.package_weight = "0.0090 g";
            PAKs.SLG46533.pattern_id_address = 1840;
            PAKs.SLG46533.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 07
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 08
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 09
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                               },    // 11
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 1  },    // 13
                new PIN() { PT = "GPIO_OE", RV = 1114, UD = 1113, IM = 1116, OM = 1118, OE = 0328,            VS = 1  },    // 14
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 1  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 1  },    // 16
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 1  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 1  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 1  },    // 19
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 1  },    // 20
            };
            PAKs.SLG46533.cnt = new CNT[]
            {
                    new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                    new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                    new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                    new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                    new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                    new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                    new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46533.acmp = new ACMP[]
            {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                    new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46533.RC_osc_freq = 25000;
            PAKs.SLG46533.RC_osc_freq_alt = 2000000;
            PAKs.SLG46533.RC_osc_src = 1342;
            PAKs.SLG46533.RC_osc_pre_div = 1339;
            PAKs.SLG46533.RING_osc_freq = 25000000;
            PAKs.SLG46533.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46534
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46534()
        {
            PAKs.SLG46534 = new PAK();
            PAKs.SLG46534.base_die = "SLG46534";
            PAKs.SLG46534.package = "STQFN-14L_c";
            PAKs.SLG46534.package_size = "2mm x 2.2mm";
            PAKs.SLG46534.PAK_family = 5;
            PAKs.SLG46534.package_weight = "0.0067 g";
            PAKs.SLG46534.pattern_id_address = 1840;
            PAKs.SLG46534.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 05
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 06
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 07
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 08
                new PIN() { PT = "GND",                                                                               },    // 09
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 1  },    // 10
                new PIN() { PT = "GPIO_OE", RV = 1114, UD = 1113, IM = 1116, OM = 1118, OE = 0328,            VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 1  },    // 12
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 1  },    // 13
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 1  },    // 14
            };
            PAKs.SLG46534.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46534.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
            };
            PAKs.SLG46534.RC_osc_freq = 25000;
            PAKs.SLG46534.RC_osc_freq_alt = 2000000;
            PAKs.SLG46534.RC_osc_src = 1342;
            PAKs.SLG46534.RC_osc_pre_div = 1339;
            PAKs.SLG46534.RING_osc_freq = 25000000;
            PAKs.SLG46534.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46535
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46535()
        {
            PAKs.SLG46535 = new PAK();
            PAKs.SLG46535.base_die = "SLG46535";
            PAKs.SLG46535.package = "STQFN-14L_c";
            PAKs.SLG46535.package_size = "2mm x 2.2mm";
            PAKs.SLG46535.PAK_family = 5;
            PAKs.SLG46535.package_weight = "0.0068 g";
            PAKs.SLG46535.dual_supply_PAK = true;
            PAKs.SLG46535.dual_supply_vdd_pins = "PIN2-PIN8";
            PAKs.SLG46535.dual_supply_vdd2_pins = "PIN10-PIN14";
            PAKs.SLG46535.pattern_id_address = 1840;
            PAKs.SLG46535.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 05
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 06
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 07
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 08
                new PIN() { PT = "GND",                                                                               },    // 09
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 2  },    // 10
                new PIN() { PT = "VDD2",                                                                              },    // 11
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 2  },    // 12
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 2  },    // 13
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 2  },    // 14
            };
            PAKs.SLG46535.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46535.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
            };
            PAKs.SLG46535.RC_osc_freq = 25000;
            PAKs.SLG46535.RC_osc_freq_alt = 2000000;
            PAKs.SLG46535.RC_osc_src = 1342;
            PAKs.SLG46535.RC_osc_pre_div = 1339;
            PAKs.SLG46535.RING_osc_freq = 25000000;
            PAKs.SLG46535.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46536
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46536()
        {
            PAKs.SLG46536 = new PAK();
            PAKs.SLG46536.base_die = "SLG46536";
            PAKs.SLG46536.package = "STQFN-14L_c";
            PAKs.SLG46536.package_size = "2mm x 2.2mm";
            PAKs.SLG46536.PAK_family = 5;
            PAKs.SLG46536.package_weight = "0.0066 g";
            PAKs.SLG46536.pattern_id_address = 1840;
            PAKs.SLG46536.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028,            IM = 1030,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 05
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 06
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 07
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 08
                new PIN() { PT = "GND",                                                                               },    // 09
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 1  },    // 10
                new PIN() { PT = "GPIO_OE", RV = 1114, UD = 1113, IM = 1116, OM = 1118, OE = 0328,            VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 1  },    // 12
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 1  },    // 13
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 1  },    // 14
            };
            PAKs.SLG46536.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46536.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
            };
            PAKs.SLG46536.RC_osc_freq = 25000;
            PAKs.SLG46536.RC_osc_freq_alt = 2000000;
            PAKs.SLG46536.RC_osc_src = 1342;
            PAKs.SLG46536.RC_osc_pre_div = 1339;
            PAKs.SLG46536.RING_osc_freq = 25000000;
            PAKs.SLG46536.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG50003
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG50003()
        {
            PAKs.SLG50003 = new PAK();
            PAKs.SLG50003.base_die = "SLG50003";
            PAKs.SLG50003.package = "STQFN-22L";
            PAKs.SLG50003.package_size = "2mm x 3mm";
            PAKs.SLG50003.PAK_family = 5;
            PAKs.SLG50003.LDO_PAK = true;
            PAKs.SLG50003.package_weight = "0.00xx g";
            PAKs.SLG50003.pattern_id_address = 1840;
            PAKs.SLG50003.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "GPIO_OE", RV = 1026, UD = 1025, IM = 1028, OM = 1030, OE = 0328,            VS = 1  },    // 01
                new PIN() { PT = "GPIO",    RV = 1035, UD = 1034, IO = 1037, DR = 1033,                       VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1051, UD = 1050, IO = 1053, DR = 1049,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1058, UD = 1057, IM = 1060, OM = 1062, OE = 0368,            VS = 1  },    // 05
                new PIN() { PT = "GPI",     RV = 1068, IM = 1070,                                             VS = 1  },    // 06
                new PIN() { PT = "VDD",                                                                               },    // 07
                new PIN() { PT = "I2C",     RV = 0000, UD = 0000, IO = 0000, DR = 0000,                       VS = 1  },    // 08   //###
                new PIN() { PT = "I2C",     RV = 0000, UD = 0000, IO = 0000, DR = 0000,                       VS = 1  },    // 09   //###
                new PIN() { PT = "GPIO_OE", RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0384,            VS = 1  },    // 10
                new PIN() { PT = "LDO_OUT", name = "LDO_VOUT0"                                                        },    // 11
                new PIN() { PT = "LDO_IN",  name = "LDO_VIN0"                                                         },    // 12
                new PIN() { PT = "LDO_OUT", name = "LDO_VOUT1"                                                        },    // 13
                new PIN() { PT = "LDO_OUT", name = "LDO_VOUT2"                                                        },    // 14
                new PIN() { PT = "LDO_IN",  name = "LDO_VIN1"                                                         },    // 15
                new PIN() { PT = "LDO_OUT", name = "LDO_VOUT3"                                                        },    // 16
                new PIN() { PT = "LDO_GND", name = "LDO_GND"                                                          },    // 17
                new PIN() { PT = "GPIO",    RV = 1099, UD = 1098, IO = 1101, DR = 1097,                       VS = 1  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0408,            VS = 1  },    // 19
                new PIN() { PT = "GND",                                                                               },    // 20
            };
            PAKs.SLG50003.cnt = new CNT[]
            {
                    new CNT() { CK = 1242, DA = 1536, LN = 8,  MD = 1246, SL = 1143 },    // 0
                    new CNT() { CK = 1250, DA = 1544, LN = 8,  MD = 1254, SL = 1142 },    // 1
                    new CNT() { CK = 1258, DA = 1552, LN = 8,  MD = 1262, SL = 1141 },    // 2
                    new CNT() { CK = 1266, DA = 1560, LN = 8,  MD = 1270, SL = 1140 },    // 3
                    new CNT() { CK = 1274, DA = 1568, LN = 8,  MD = 1278, SL = 1139 },    // 4
            };
            PAKs.SLG50003.acmp = new ACMP[]
            {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1631, HY = 1118 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1114 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1125 },    // 2
                    new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1121 },    // 3
            };
            PAKs.SLG50003.RC_osc_freq = 25000;
            PAKs.SLG50003.RC_osc_freq_alt = 2000000;
            PAKs.SLG50003.RC_osc_src = 1294;
            PAKs.SLG50003.RC_osc_pre_div = 1291;
            PAKs.SLG50003.LF_osc_freq = 2000;
            PAKs.SLG50003.LF_osc_freq = 1288;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46533M
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46533M()
        {
            PAKs.SLG46533M = new PAK();
            PAKs.SLG46533M.base_die = "SLG46533M";
            PAKs.SLG46533M.package = "MSTQFN-22L";
            PAKs.SLG46533M.package_size = "2mm x 3mm";
            PAKs.SLG46533M.PAK_family = 5;
            PAKs.SLG46533M.package_weight = "0.0058 g";
            PAKs.SLG46533M.pattern_id_address = 1840;
            PAKs.SLG46533M.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 01
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 05
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 06
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 07
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 08
                new PIN() { PT = "NA",                                                                                },    // 09
                new PIN() { PT = "NA",                                                                                },    // 10
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", RV = 1114, UD = 1113, IM = 1116, OM = 1118, OE = 0328,            VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 1  },    // 13
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 1  },    // 14
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 1  },    // 15
                new PIN() { PT = "VDD",                                                                               },    // 16
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 1  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 1  },    // 18
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 19
                new PIN() { PT = "GND",                                                                               },    // 20
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 1  },    // 21
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 1  },    // 22
          };
            PAKs.SLG46533M.cnt = new CNT[]
            {
                    new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                    new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                    new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                    new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                    new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                    new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                    new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46533M.acmp = new ACMP[]
            {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                    new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46533M.RC_osc_freq = 25000;
            PAKs.SLG46533M.RC_osc_freq_alt = 2000000;
            PAKs.SLG46533M.RC_osc_src = 1342;
            PAKs.SLG46533M.RC_osc_pre_div = 1339;
            PAKs.SLG46533M.RING_osc_freq = 25000000;
            PAKs.SLG46533M.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46537
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46537()
        {
            PAKs.SLG46537 = new PAK();
            PAKs.SLG46537.base_die = "SLG46537";
            PAKs.SLG46537.package = "STQFN-20L";
            PAKs.SLG46537.package_size = "2mm x 3mm";
            PAKs.SLG46537.PAK_family = 5;
            PAKs.SLG46537.package_weight = "0.0090 g";
            PAKs.SLG46537.pattern_id_address = 1840;
            PAKs.SLG46537.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 07
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 08
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 09
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                               },    // 11
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 1  },    // 13
                new PIN() { PT = "GPIO_OE", RV = 1114, UD = 1113, IM = 1116, OM = 1118, OE = 0328,            VS = 1  },    // 14
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 1  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 1  },    // 16
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 1  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 1  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 1  },    // 19
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 1  },    // 20
            };
            PAKs.SLG46537.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46537.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46537.RC_osc_freq = 25000;
            PAKs.SLG46537.RC_osc_freq_alt = 2000000;
            PAKs.SLG46537.RC_osc_src = 1342;
            PAKs.SLG46537.RC_osc_pre_div = 1339;
            PAKs.SLG46537.RING_osc_freq = 25000000;
            PAKs.SLG46537.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46538
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46538()
        {
            PAKs.SLG46538 = new PAK();
            PAKs.SLG46538.base_die = "SLG46538";
            PAKs.SLG46538.package = "STQFN-20L";
            PAKs.SLG46538.package_size = "2mm x 3mm";
            PAKs.SLG46538.PAK_family = 5;
            PAKs.SLG46538.package_weight = "0.0090 g";
            PAKs.SLG46538.pattern_id_address = 1840;
            PAKs.SLG46538.dual_supply_PAK = true;
            PAKs.SLG46538.dual_supply_vdd_pins = "PIN2-PIN10";
            PAKs.SLG46538.dual_supply_vdd2_pins = "PIN12-PIN20";
            PAKs.SLG46538.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 07
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 08
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 09
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                               },    // 11
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 2  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 2  },    // 13
                new PIN() { PT = "VDD2",                                                                              },    // 14
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 2  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 2  },    // 16
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 2  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 2  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 2  },    // 19
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 2  },    // 20
            };
            PAKs.SLG46538.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46538.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46538.RC_osc_freq = 25000;
            PAKs.SLG46538.RC_osc_freq_alt = 2000000;
            PAKs.SLG46538.RC_osc_src = 1342;
            PAKs.SLG46538.RC_osc_pre_div = 1339;
            PAKs.SLG46538.RING_osc_freq = 25000000;
            PAKs.SLG46538.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46537M
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46537M()
        {
            PAKs.SLG46537M = new PAK();
            PAKs.SLG46537M.base_die = "SLG46537M";
            PAKs.SLG46537M.package = "MSTQFN-22L";
            PAKs.SLG46537M.package_size = "2mm x 2.2mm";
            PAKs.SLG46537M.PAK_family = 5;
            PAKs.SLG46537M.package_weight = "0.0058 g";
            PAKs.SLG46537M.pattern_id_address = 1840;
            PAKs.SLG46537M.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 01
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 05
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 06
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 07
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 08
                new PIN() { PT = "NA",                                                                                },    // 09
                new PIN() { PT = "NA",                                                                                },    // 10
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", RV = 1114, UD = 1113, IM = 1116, OM = 1118, OE = 0328,            VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 1  },    // 13
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 1  },    // 14
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 1  },    // 15
                new PIN() { PT = "VDD",                                                                               },    // 16
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 1  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 1  },    // 18
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 19
                new PIN() { PT = "GND",                                                                               },    // 20
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 1  },    // 21
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 1  },    // 22
            };
            PAKs.SLG46537M.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46537M.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46537M.RC_osc_freq = 25000;
            PAKs.SLG46537M.RC_osc_freq_alt = 2000000;
            PAKs.SLG46537M.RC_osc_src = 1342;
            PAKs.SLG46537M.RC_osc_pre_div = 1339;
            PAKs.SLG46537M.RING_osc_freq = 25000000;
            PAKs.SLG46537M.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46538M
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46538M()
        {
            PAKs.SLG46538M = new PAK();
            PAKs.SLG46538M.base_die = "SLG46538M";
            PAKs.SLG46538M.package = "MSTQFN-22L";
            PAKs.SLG46538M.package_size = "2mm x 2.2mm";
            PAKs.SLG46538M.PAK_family = 5;
            PAKs.SLG46538M.package_weight = "0.0058 g";
            PAKs.SLG46538M.pattern_id_address = 1840;
            PAKs.SLG46538M.dual_supply_PAK = true;
            PAKs.SLG46538M.dual_supply_vdd_pins = "IOs 0, 1, 2, 3, 4, 5, 6, 7, 8";
            PAKs.SLG46538M.dual_supply_vdd2_pins = "IOs 9, 10, 12, 13, 14, 15, 16, 17";
            PAKs.SLG46538M.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "GPI",     RV = 1028, IM = 1030,                                             VS = 1  },    // 01
                new PIN() { PT = "GPIO_OE", RV = 1034, UD = 1033, IM = 1036, OM = 1038, OE = 0208,            VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 1043, UD = 1042, IO = 1045, DR = 1041,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", RV = 1050, UD = 1049, IM = 1052, OM = 1054, OE = 0232,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",    RV = 1059, UD = 1058, IO = 1061, DR = 1057,                       VS = 1  },    // 05
                new PIN() { PT = "GPIO_OE", RV = 1066, UD = 1065, IM = 1068, OM = 1070, OE = 0256,            VS = 1  },    // 06
                new PIN() { PT = "I2C",     RV = 1083, UD = 1082, IO = 1085, DR = 1081,                       VS = 1  },    // 07
                new PIN() { PT = "SD_OE",   RV = 1090, UD = 1089, IM = 1092, OM = 1094, OE = 0288, SD = 1088, VS = 1  },    // 08
                new PIN() { PT = "NA",                                                                                },    // 09
                new PIN() { PT = "NA",                                                                                },    // 10
                new PIN() { PT = "GPIO_OE", RV = 1106, UD = 1105, IM = 1108, OM = 1110, OE = 0312,            VS = 2  },    // 11
                new PIN() { PT = "VDD2",                                                                              },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1130, UD = 1129, IM = 1132, OM = 1134, OE = 0352,            VS = 2  },    // 13
                new PIN() { PT = "GPIO",    RV = 1139, UD = 1138, IO = 1141, DR = 1137,                       VS = 2  },    // 14
                new PIN() { PT = "GPIO_OE", RV = 1154, UD = 1153, IM = 1156, OM = 1158, OE = 0392,            VS = 2  },    // 15
                new PIN() { PT = "VDD",                                                                               },    // 16
                new PIN() { PT = "GPIO",    RV = 1163, UD = 1162, IO = 1165, DR = 1161,                       VS = 2  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1146, UD = 1145, IM = 1148, OM = 1150, OE = 0376,            VS = 2  },    // 18
                new PIN() { PT = "I2C",     RV = 1075, UD = 1074, IO = 1077, DR = 1073,                       VS = 1  },    // 19
                new PIN() { PT = "GND",                                                                               },    // 20
                new PIN() { PT = "SD",      RV = 1099, UD = 1098, IO = 1101, DR = 1097,            SD = 1096, VS = 2  },    // 21
                new PIN() { PT = "GPIO",    RV = 1123, UD = 1122, IO = 1125, DR = 1121,                       VS = 2  },    // 22
            };
            PAKs.SLG46538M.cnt = new CNT[]
            {
                new CNT() { CK = 1314, DA = 1576, LN = 16, MD = 1318, SL = 1193 },    // 0
                new CNT() { CK = 1322, DA = 1592, LN = 16, MD = 1326, SL = 1192 },    // 1
                new CNT() { CK = 1274, DA = 1536, LN = 8,  MD = 1278, SL = 1198 },    // 2
                new CNT() { CK = 1282, DA = 1544, LN = 8,  MD = 1286, SL = 1197 },    // 3
                new CNT() { CK = 1290, DA = 1552, LN = 8,  MD = 1294, SL = 1196 },    // 4
                new CNT() { CK = 1298, DA = 1560, LN = 8,  MD = 1302, SL = 1195 },    // 5
                new CNT() { CK = 1306, DA = 1568, LN = 8,  MD = 1310, SL = 1194 },    // 6
            };
            PAKs.SLG46538M.acmp = new ACMP[]
            {
                new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
            };
            PAKs.SLG46538M.RC_osc_freq = 25000;
            PAKs.SLG46538M.RC_osc_freq_alt = 2000000;
            PAKs.SLG46538M.RC_osc_src = 1342;
            PAKs.SLG46538M.RC_osc_pre_div = 1339;
            PAKs.SLG46538M.RING_osc_freq = 25000000;
            PAKs.SLG46538M.RING_osc_pre_div = 1336;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46140
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46140()
        {
            PAKs.SLG46140 = new PAK();
            PAKs.SLG46140.base_die = "SLG46140";
            PAKs.SLG46140.package = "STQFN-14L_a";
            PAKs.SLG46140.package_size = "1.6mm x 2mm";
            PAKs.SLG46140.PAK_family = 4;
            PAKs.SLG46140.package_weight = "0.00xx g";  //###
            PAKs.SLG46140.pattern_id_address = 1007;
            PAKs.SLG46140.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                        VS = 0  },    // 00
                new PIN() { PT = "VDD",                                                                       VS = 0  },    // 01
                new PIN() { PT = "GPI",     RV = 0763, UD = 0765, IM = 0761,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 0770, UD = 0772, IM = 0766, OM = 0768, OE = 0270,            VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", RV = 0777, UD = 0779, IM = 0773, OM = 0775, OE = 0282,            VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 0784, UD = 0786, IM = 0780, OM = 0782, OE = 0294,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 0791, UD = 0793, IO = 0788, DR = 0794,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 0799, UD = 0801, IM = 0795, OM = 0797, OE = 0312,            VS = 1  },    // 07
                new PIN() { PT = "GND",                                                                               },    // 08
                new PIN() { PT = "SD_OE",   RV = 0806, UD = 0808, IM = 0802, OM = 0804, OE = 0324, SD = 0809, VS = 1  },    // 09
                new PIN() { PT = "SD",      RV = 0814, UD = 0816, IO = 0811, DR = 0817,            SD = 0818, VS = 1  },    // 10
                new PIN() { PT = "GPIO",    RV = 0823, UD = 0825, IO = 0820, DR = 0826,                       VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", RV = 0831, UD = 0833, IM = 0827, OM = 0829, OE = 0348,            VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 0838, UD = 0840, IM = 0834, OM = 0836, OE = 0360,            VS = 1  },    // 13
                new PIN() { PT = "GPIO_OE", RV = 0845, UD = 0847, IM = 0841, OM = 0843, OE = 0372,            VS = 1  },    // 14
            };
            PAKs.SLG46140.cnt = new CNT[]
            {
                new CNT() { CK = 0737, DA = 0722, LN = 16, MD = 0743, SL = 0743, CS = 4 },    // 0
                new CNT() { CK = 0714, DA = 0705, LN = 08, MD = 0720, SL = 0720, CS = 4 },    // 1
                new CNT() { CK = 0695, DA = 0680, LN = 08, MD = 0701, SL = 0701, CS = 4 },    // 2
                new CNT() { CK = 0670, DA = 0661, LN = 08, MD = 0676, SL = 0676, CS = 4 },    // 3
            };
            PAKs.SLG46140.acmp = new ACMP[]
            {
                new ACMP() { TH = 0496, HY = 0510, GN = 0522, LB = 0524 },    // 0
                new ACMP() { TH = 0501, HY = 0508, GN = 0519, LB = 0518 },    // 1
            };
            PAKs.SLG46140.LF_osc_freq = 1743;
            PAKs.SLG46140.LF_osc_pre_div = 560;
            PAKs.SLG46140.RC_osc_freq = 25000;
            PAKs.SLG46140.RC_osc_freq_alt = 2000000;
            PAKs.SLG46140.RC_osc_src = 565;
            PAKs.SLG46140.RC_osc_pre_div = 568;
            PAKs.SLG46140.RING_osc_freq = 27250000;
            PAKs.SLG46140.RING_osc_pre_div = 576;
            PAKs.SLG46140.lock_status = 1015;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46620
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46620()
        {
            PAKs.SLG46620 = new PAK();
            PAKs.SLG46620.base_die = "SLG46620";
            PAKs.SLG46620.package = "STQFN-20L";
            PAKs.SLG46620.package_size = "2mm x 3mm";
            PAKs.SLG46620.PAK_family = 4;
            PAKs.SLG46620.package_weight = "0.015 g";
            PAKs.SLG46620.pattern_id_address = 2031;
            PAKs.SLG46620.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                        VS = 0  },    // 00
                new PIN() { PT = "VDD",                                                                       VS = 0  },    // 01
                new PIN() { PT = "GPI",     RV = 0943, UD = 0945, IM = 0941,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 0950, UD = 0952, IM = 0946, OM = 0948, OE = 0342,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 0956, UD = 0958, IO = 0953, DR = 0959,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 0964, UD = 0966, IM = 0960, OM = 0962, OE = 0360,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 0970, UD = 0972, IO = 0967, DR = 0973,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 0978, UD = 0979, IM = 0974, OM = 0976, OE = 0378,            VS = 1  },    // 07
                new PIN() { PT = "GPIO",    RV = 0984, UD = 0985, IO = 0981, DR = 0987,                       VS = 1  },    // 08
                new PIN() { PT = "GPIO_OE", RV = 0992, UD = 0994, IM = 0988, OM = 0990, OE = 0396,            VS = 1  },    // 09
                new PIN() { PT = "SD_OE",   RV = 0999, UD = 1001, IM = 0995, OM = 0997, OE = 0408, SD = 1002, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                       VS = 0  },    // 11
                new PIN() { PT = "SD",      RV = 1914, UD = 1916, IO = 1911, DR = 1917,            SD = 1918, VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1923, UD = 1925, IM = 1919, OM = 1921, OE = 1372,            VS = 1  },    // 13
                new PIN() { PT = "GPIO_OE", RV = 1930, UD = 1932, IM = 1926, OM = 1928, OE = 1384,            VS = 1  },    // 14
                new PIN() { PT = "GPIO",    RV = 1936, UD = 1938, IO = 1933, DR = 1939,                       VS = 1  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 1944, UD = 1946, IM = 1940, OM = 1942, OE = 1402,            VS = 1  },    // 16
                new PIN() { PT = "GPIO",    RV = 1950, UD = 1952, IO = 1947, DR = 1953,                       VS = 1  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1958, UD = 1960, IM = 1954, OM = 1956, OE = 1420,            VS = 1  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1965, UD = 1967, IM = 1961, OM = 1963, OE = 1432,            VS = 1  },    // 19
                new PIN() { PT = "GPIO",    RV = 1971, UD = 1973, IO = 1968, DR = 1974,                       VS = 1  },    // 20
            };
            PAKs.SLG46620.cnt = new CNT[]
            {
                new CNT() { CK = 1745, DA = 1731, LN = 14, MD = 1750, SL = 1750, CS = 3 },    // 0
                new CNT() { CK = 1767, DA = 1753, LN = 14, MD = 1772, SL = 1772, CS = 3 },    // 1
                new CNT() { CK = 1788, DA = 1774, LN = 14, MD = 1794, SL = 1794, CS = 4 },    // 2
                new CNT() { CK = 1813, DA = 1799, LN = 14, MD = 1818, SL = 1818, CS = 3 },    // 3
                new CNT() { CK = 1828, DA = 1820, LN = 08, MD = 1834, SL = 0000, CS = 4 },    // 4
                new CNT() { CK = 1846, DA = 1838, LN = 08, MD = 1851, SL = 0000, CS = 3 },    // 5
                new CNT() { CK = 1860, DA = 1852, LN = 08, MD = 1865, SL = 0000, CS = 3 },    // 6
                new CNT() { CK = 1874, DA = 1866, LN = 08, MD = 1879, SL = 0000, CS = 3 },    // 7
                new CNT() { CK = 1888, DA = 1880, LN = 08, MD = 1894, SL = 0000, CS = 4 },    // 8
                new CNT() { CK = 1903, DA = 1895, LN = 08, MD = 1909, SL = 0000, CS = 4 },    // 9
            };
            PAKs.SLG46620.acmp = new ACMP[]
            {
                new ACMP() { TH = 0892, HY = 0934, GN = 0853, LB = 0852 },  // 0
                new ACMP() { TH = 0897, HY = 0932, GN = 0857, LB = 0861 },  // 1
                new ACMP() { TH = 0902, HY = 0930, GN = 0864, LB = 0862 },  // 2
                new ACMP() { TH = 0907, HY = 0928, GN = 0867, LB = 0866 },  // 3
                new ACMP() { TH = 0912, HY = 0926, GN = 0871, LB = 0875 },  // 4
                new ACMP() { TH = 0917, HY = 0924, GN = 0000, LB = 0880 },  // 5
            };
            PAKs.SLG46620.LF_osc_freq = 1743;
            PAKs.SLG46620.LF_osc_pre_div = 1654;
            PAKs.SLG46620.RC_osc_freq = 25000;
            PAKs.SLG46620.RC_osc_freq_alt = 2000000;
            PAKs.SLG46620.RC_osc_src = 1650;
            PAKs.SLG46620.RC_osc_pre_div = 1643;
            PAKs.SLG46620.RING_osc_freq = 27250000;
            PAKs.SLG46620.RING_osc_pre_div = 1635;
            PAKs.SLG46620.lock_status = 2039;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46621
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46621()
        {
            PAKs.SLG46621 = new PAK();
            PAKs.SLG46621.base_die = "SLG46621";
            PAKs.SLG46621.package = "STQFN-20L";
            PAKs.SLG46621.package_size = "2mm x 3mm";
            PAKs.SLG46621.PAK_family = 4;
            PAKs.SLG46621.package_weight = "0.015 g";
            PAKs.SLG46621.pattern_id_address = 2031;
            PAKs.SLG46621.dual_supply_PAK = true;
            PAKs.SLG46621.dual_supply_vdd_pins = "PIN2-PIN10";
            PAKs.SLG46621.dual_supply_vdd2_pins = "PIN12-PIN20";
            PAKs.SLG46621.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 0943, UD = 0945, IM = 0941,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 0950, UD = 0952, IM = 0946, OM = 0948, OE = 0342,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 0956, UD = 0958, IO = 0953, DR = 0959,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 0964, UD = 0966, IM = 0960, OM = 0962, OE = 0360,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 0970, UD = 0972, IO = 0967, DR = 0973,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 0978, UD = 0979, IM = 0974, OM = 0976, OE = 0378,            VS = 1  },    // 07
                new PIN() { PT = "GPIO",    RV = 0984, UD = 0985, IO = 0981, DR = 0987,                       VS = 1  },    // 08
                new PIN() { PT = "GPIO_OE", RV = 0992, UD = 0994, IM = 0988, OM = 0990, OE = 0396,            VS = 1  },    // 09
                new PIN() { PT = "SD_OE",   RV = 0999, UD = 1001, IM = 0995, OM = 0997, OE = 0408, SD = 1002, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                               },    // 11
                new PIN() { PT = "SD",      RV = 1914, UD = 1916, IO = 1911, DR = 1917,            SD = 1918, VS = 2  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 1923, UD = 1925, IM = 1919, OM = 1921, OE = 1372,            VS = 2  },    // 13
                new PIN() { PT = "VDD2",                                                                              },    // 14
                new PIN() { PT = "GPIO",    RV = 1936, UD = 1938, IO = 1933, DR = 1939,                       VS = 2  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 1944, UD = 1946, IM = 1940, OM = 1942, OE = 1402,            VS = 2  },    // 16
                new PIN() { PT = "GPIO",    RV = 1950, UD = 1952, IO = 1947, DR = 1953,                       VS = 2  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 1958, UD = 1960, IM = 1954, OM = 1956, OE = 1420,            VS = 2  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 1965, UD = 1967, IM = 1961, OM = 1963, OE = 1432,            VS = 2  },    // 19
                new PIN() { PT = "GPIO",    RV = 1971, UD = 1973, IO = 1968, DR = 1974,                       VS = 2  },    // 20
            };
            PAKs.SLG46621.cnt = new CNT[]
            {
                new CNT() { CK = 1745, DA = 1731, LN = 14, MD = 1750, SL = 1750, CS = 3 },    // 0
                new CNT() { CK = 1767, DA = 1753, LN = 14, MD = 1772, SL = 1772, CS = 3 },    // 1
                new CNT() { CK = 1788, DA = 1774, LN = 14, MD = 1794, SL = 1794, CS = 4 },    // 2
                new CNT() { CK = 1813, DA = 1799, LN = 14, MD = 1818, SL = 1818, CS = 3 },    // 3
                new CNT() { CK = 1828, DA = 1820, LN = 08, MD = 1834, SL = 0000, CS = 4 },    // 4
                new CNT() { CK = 1846, DA = 1838, LN = 08, MD = 1851, SL = 0000, CS = 3 },    // 5
                new CNT() { CK = 1860, DA = 1852, LN = 08, MD = 1865, SL = 0000, CS = 3 },    // 6
                new CNT() { CK = 1874, DA = 1866, LN = 08, MD = 1879, SL = 0000, CS = 3 },    // 7
                new CNT() { CK = 1888, DA = 1880, LN = 08, MD = 1894, SL = 0000, CS = 4 },    // 8
                new CNT() { CK = 1903, DA = 1895, LN = 08, MD = 1909, SL = 0000, CS = 4 },    // 9
            };
            PAKs.SLG46621.acmp = new ACMP[]
            {
                new ACMP() { TH = 0892, HY = 0934, GN = 0853, LB = 0852 },  // 0
                new ACMP() { TH = 0897, HY = 0932, GN = 0857, LB = 0861 },  // 1
                new ACMP() { TH = 0902, HY = 0930, GN = 0864, LB = 0862 },  // 2
                new ACMP() { TH = 0907, HY = 0928, GN = 0867, LB = 0866 },  // 3
                new ACMP() { TH = 0912, HY = 0926, GN = 0871, LB = 0875 },  // 4
                new ACMP() { TH = 0917, HY = 0924, GN = 0000, LB = 0880 },  // 5
            };
            PAKs.SLG46621.LF_osc_freq = 1743;
            PAKs.SLG46621.LF_osc_pre_div = 1654;
            PAKs.SLG46621.RC_osc_freq = 25000;
            PAKs.SLG46621.RC_osc_freq_alt = 2000000;
            PAKs.SLG46621.RC_osc_src = 1650;
            PAKs.SLG46621.RC_osc_pre_div = 1643;
            PAKs.SLG46621.RING_osc_freq = 27250000;
            PAKs.SLG46621.RING_osc_pre_div = 1635;
            PAKs.SLG46621.lock_status = 2039;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46721
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46721()
        {
            PAKs.SLG46721 = new PAK();
            PAKs.SLG46721.base_die = "SLG46721";
            PAKs.SLG46721.package = "STQFN-20L";
            PAKs.SLG46721.package_size = "2mm x 3mm";
            PAKs.SLG46721.PAK_family = 3;
            PAKs.SLG46721.package_weight = "0.015 g";
            PAKs.SLG46721.pattern_id_address = 970;
            PAKs.SLG46721.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 0831,            IM = 0829,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO_OE", RV = 0837, UD = 0839, IM = 0833, OM = 0835, OE = 0006,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    RV = 0843, UD = 0845, IO = 0840, DR = 0846,                       VS = 1  },    // 04
                new PIN() { PT = "GPIO_OE", RV = 0851, UD = 0853, IM = 0847, OM = 0849, OE = 0024,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",    RV = 0857, UD = 0859, IO = 0854, DR = 0860,                       VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 0865, UD = 0867, IM = 0861, OM = 0863, OE = 0042,            VS = 1  },    // 07
                new PIN() { PT = "GPIO",    RV = 0871, UD = 0873, IO = 0867, DR = 0874,                       VS = 1  },    // 08
                new PIN() { PT = "GPIO_OE", RV = 0879, UD = 0881, IM = 0875, OM = 0877, OE = 0060,            VS = 1  },    // 09
                new PIN() { PT = "SD",      RV = 0885, UD = 0887, IO = 0882, DR = 0888,            SD = 0889, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                               },    // 11
                new PIN() { PT = "SD",      RV = 0893, UD = 0895, IO = 0890, DR = 0896,            SD = 0897, VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 0902, UD = 0904, IM = 0898, OM = 0900, OE = 0468,            VS = 1  },    // 13
                new PIN() { PT = "GPIO_OE", RV = 0909, UD = 0911, IM = 0905, OM = 0907, OE = 0480,            VS = 1  },    // 14
                new PIN() { PT = "GPIO",    RV = 0915, UD = 0917, IO = 0912, DR = 0918,                       VS = 1  },    // 15
                new PIN() { PT = "GPIO_OE", RV = 0923, UD = 0925, IM = 0919, OM = 0921, OE = 0498,            VS = 1  },    // 16
                new PIN() { PT = "GPIO",    RV = 0929, UD = 0931, IO = 0926, DR = 0932,                       VS = 1  },    // 17
                new PIN() { PT = "GPIO_OE", RV = 0937, UD = 0939, IM = 0933, OM = 0935, OE = 0516,            VS = 1  },    // 18
                new PIN() { PT = "GPIO_OE", RV = 0944, UD = 0946, IM = 0940, OM = 0942, OE = 0528,            VS = 1  },    // 19
                new PIN() { PT = "GPIO",    RV = 0950, UD = 0952, IO = 0947, DR = 0953,                       VS = 1  },    // 20
            };
            PAKs.SLG46721.cnt = new CNT[]
            {
                new CNT() { CK = 0714, DA = 0717, LN = 14, MD = 0713, SL = 0000 },    // 0
                new CNT() { CK = 0734, DA = 0737, LN = 14, MD = 0733, SL = 0000 },    // 1
                new CNT() { CK = 0674, DA = 0677, LN = 08, MD = 0673, SL = 0705 },    // 2
                new CNT() { CK = 0690, DA = 0693, LN = 08, MD = 0689, SL = 0706 },    // 3
                new CNT() { CK = 0754, DA = 0757, LN = 08, MD = 0753, SL = 0000 },    // 4
                new CNT() { CK = 0768, DA = 0771, LN = 08, MD = 0767, SL = 0000 },    // 5
                new CNT() { CK = 0782, DA = 0785, LN = 08, MD = 0781, SL = 0000 },    // 6
            };
            PAKs.SLG46721.acmp = new ACMP[]
            {
                new ACMP() { TH = 0795, HY = 0800, GN = 0802, LB = 0804 },  // 0
                new ACMP() { TH = 0806, HY = 0811, GN = 0813, LB = 0816 },  // 1
                new ACMP() { TH = 0818, HY = 0823, GN = 0825, LB = 0827 },  // 2
                new ACMP() { TH = 0552, HY = 0557, GN = 0559, LB = 0561 },  // 3
            };
            PAKs.SLG46721.RC_osc_freq = 25000;
            PAKs.SLG46721.RC_osc_freq_alt = 2000000;
            PAKs.SLG46721.RC_osc_src = 0708;
            PAKs.SLG46721.RC_osc_pre_div = 0565;
            PAKs.SLG46721.lock_status = 979;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46722
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46722()
        {
            PAKs.SLG46722 = new PAK();
            PAKs.SLG46722.base_die = "SLG46722";
            PAKs.SLG46722.package = "STQFN-20L";
            PAKs.SLG46722.package_size = "2mm x 3mm";
            PAKs.SLG46722.PAK_family = 3;
            PAKs.SLG46722.package_weight = "0.015 g";
            PAKs.SLG46722.pattern_id_address = 992;
            PAKs.SLG46722.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "VDD",                                                                    },    // 01
                new PIN() { PT = "GPI",    IM = 0844,  RV = 0846,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",   IO = 0848,  RV = 0851, UD = 0853, DR = 0854,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",   IO = 0855,  RV = 0889, UD = 0860, DR = 0861,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",   IO = 0862,  RV = 0865, UD = 0867, DR = 0868,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",   IO = 0869,  RV = 0872, UD = 0874, DR = 0875,            VS = 1  },    // 06
                new PIN() { PT = "GPIO",   IO = 0876,  RV = 0879, UD = 0881, DR = 0882,            VS = 1  },    // 07
                new PIN() { PT = "GPIO",   IO = 0883,  RV = 0886, UD = 0888, DR = 0889,            VS = 1  },    // 08
                new PIN() { PT = "GPIO",   IO = 0890,  RV = 0893, UD = 0895, DR = 0896,            VS = 1  },    // 09
                new PIN() { PT = "SD",     IO = 0897,  RV = 0900, UD = 0902, DR = 0903, SD = 0904, VS = 1  },    // 10
                new PIN() { PT = "GND",                                                                    },    // 11
                new PIN() { PT = "SD",     IO = 0905,  RV = 0908, UD = 0910, DR = 0911, SD = 0912, VS = 1  },    // 12
                new PIN() { PT = "GPIO",   IO = 0913,  RV = 0916, UD = 0918, DR = 0919,            VS = 1  },    // 13
                new PIN() { PT = "GPIO",   IO = 0920,  RV = 0923, UD = 0925, DR = 0926,            VS = 1  },    // 14
                new PIN() { PT = "GPIO",   IO = 0927,  RV = 0930, UD = 0932, DR = 0933,            VS = 1  },    // 15
                new PIN() { PT = "GPIO",   IO = 0934,  RV = 0937, UD = 0939, DR = 0940,            VS = 1  },    // 16
                new PIN() { PT = "GPIO",   IO = 0941,  RV = 0944, UD = 0946, DR = 0947,            VS = 1  },    // 17
                new PIN() { PT = "GPIO",   IO = 0948,  RV = 0951, UD = 0953, DR = 0954,            VS = 1  },    // 18
                new PIN() { PT = "GPIO",   IO = 0955,  RV = 0958, UD = 0960, DR = 0961,            VS = 1  },    // 19
                new PIN() { PT = "GPIO",   IO = 0962,  RV = 0965, UD = 0967, DR = 0968,            VS = 1  },    // 20
            };
            PAKs.SLG46722.cnt = new CNT[]
            {
                new CNT() { CK = 0715, DA = 0717, LN = 14, MD = 0714, SL = 0000 },    // 0
                new CNT() { CK = 0735, DA = 0738, LN = 14, MD = 0734, SL = 0000 },    // 1
                new CNT() { CK = 0755, DA = 0758, LN = 08, MD = 0754, SL = 0000 },    // 2
                new CNT() { CK = 0769, DA = 0772, LN = 08, MD = 0768, SL = 0000 },    // 3
                new CNT() { CK = 0783, DA = 0786, LN = 08, MD = 0782, SL = 0000 },    // 4
                new CNT() { CK = 0797, DA = 0800, LN = 08, MD = 0796, SL = 0000 },    // 5
                new CNT() { CK = 0811, DA = 0814, LN = 08, MD = 0810, SL = 0000 },    // 6
                new CNT() { CK = 0825, DA = 0828, LN = 14, MD = 0824, SL = 0000 },    // 7
            };
            PAKs.SLG46722.acmp = new ACMP[]
            {
                new ACMP() { TH = 0795, HY = 0800, GN = 0802, LB = 0804 },  // 0
                new ACMP() { TH = 0806, HY = 0811, GN = 0813, LB = 0816 },  // 1
                new ACMP() { TH = 0818, HY = 0823, GN = 0825, LB = 0827 },  // 2
                new ACMP() { TH = 0552, HY = 0557, GN = 0559, LB = 0561 },  // 3
            };
            PAKs.SLG46722.RC_osc_freq = 25000;
            PAKs.SLG46722.RC_osc_freq_alt = 2000000;
            PAKs.SLG46722.RC_osc_src = 0970;
            PAKs.SLG46722.RC_osc_pre_div = 975;
            PAKs.SLG46722.lock_status = 979;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46110
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46110()
        {
            PAKs.SLG46110 = new PAK();
            PAKs.SLG46110.base_die = "SLG46110";
            PAKs.SLG46110.package = "STQFN-12L";
            PAKs.SLG46110.package_size = "1.6mm x 1.6mm";
            PAKs.SLG46110.PAK_family = 3;
            PAKs.SLG46110.package_weight = "0.0036 g";
            PAKs.SLG46110.pattern_id_address = 433;
            PAKs.SLG46110.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "VDD",                                                                    },    // 01
                new PIN() { PT = "GPI",     IM = 0379, RV = 0381,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",    IO = 0383, RV = 0386, UD = 0388, DR = 0389,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    IO = 0390, RV = 0393, UD = 0395, DR = 0396,            VS = 1  },    // 04
                new PIN() { PT = "NA",                                                                     },    // 05
                new PIN() { PT = "GPIO_OE", IM = 0397, OM = 0399, RV = 0401, UD = 0403, OE = 0015, VS = 1  },    // 06
                new PIN() { PT = "GND",                                                                    },    // 07
                new PIN() { PT = "GPIO",    IO = 0404, RV = 0407, UD = 0409, DR = 0410,            VS = 1  },    // 08
                new PIN() { PT = "GPIO",    IO = 0411, RV = 0414, UD = 0416, DR = 0417,            VS = 1  },    // 09
                new PIN() { PT = "GPIO_OE", IM = 0418, OM = 0420, RV = 0422, UD = 0424, OE = 0210, VS = 1  },    // 10
                new PIN() { PT = "NA",                                                                     },    // 11
                new PIN() { PT = "GPIO",    IO = 0425, RV = 0428, UD = 0430, DR = 0431,            VS = 1  },    // 12
            };
            PAKs.SLG46110.cnt = new CNT[]
            {
                new CNT() { CK = 0315, DA = 0318, LN = 08, MD = 0314, SL = 0000 },    // 0
                new CNT() { CK = 0329, DA = 0332, LN = 08, MD = 0328, SL = 0000 },    // 1
                new CNT() { CK = 0286, DA = 0289, LN = 08, MD = 0285, SL = 0301 },    // 2
                new CNT() { CK = 0343, DA = 0346, LN = 08, MD = 1342, SL = 0000 },    // 3
            };
            PAKs.SLG46110.acmp = new ACMP[]
            {
                new ACMP() { TH = 0356, HY = 0361, GN = 0363, LB = 0365 },    // 0
                new ACMP() { TH = 0367, HY = 0372, GN = 0374, LB = 0377 },    // 1
            };
            PAKs.SLG46110.RC_osc_freq = 25000;
            PAKs.SLG46110.RC_osc_freq_alt = 2000000;
            PAKs.SLG46110.RC_osc_src = 0303;
            PAKs.SLG46110.RC_osc_pre_div = 0304;
            PAKs.SLG46110.lock_status = 450;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46120
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46120()
        {
            PAKs.SLG46120 = new PAK();
            PAKs.SLG46120.base_die = "SLG46120";
            PAKs.SLG46120.package = "STQFN-12L";
            PAKs.SLG46120.package_size = "1.6mm x 1.6mm";
            PAKs.SLG46120.PAK_family = 3;
            PAKs.SLG46120.package_weight = "0.0038 g";
            PAKs.SLG46120.pattern_id_address = 691;
            PAKs.SLG46120.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "VDD",                                                                    },    // 01
                new PIN() { PT = "GPI",     IM = 0623, RV = 0625,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",    IO = 0627, RV = 0630, UD = 0632, DR = 0633,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    IO = 0634, RV = 0637, UD = 0639, DR = 0640,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",    IO = 0641, RV = 0644, UD = 0646, DR = 0647,            VS = 1  },    // 05
                new PIN() { PT = "GPIO_OE", IM = 0648, OM = 0650, RV = 0652, UD = 0654, OE = 0024, VS = 1  },    // 06
                new PIN() { PT = "GND",                                                                    },    // 07
                new PIN() { PT = "GPIO",    IO = 0655, RV = 0658, UD = 0660, DR = 0661,            VS = 1  },    // 08
                new PIN() { PT = "GPIO",    IO = 0662, RV = 0665, UD = 0667, DR = 0668,            VS = 1  },    // 09
                new PIN() { PT = "GPIO_OE", IM = 0669, OM = 0671, RV = 0673, UD = 0675, OE = 0378, VS = 1  },    // 10
                new PIN() { PT = "GPIO",    IO = 0676, RV = 0679, UD = 0681, DR = 0682,            VS = 1  },    // 11
                new PIN() { PT = "GPIO",    IO = 0683, RV = 0686, UD = 0688, DR = 0689,            VS = 1  },    // 12
            };
            PAKs.SLG46120.cnt = new CNT[]
            {
                new CNT() { CK = 0549, DA = 0552, LN = 14, MD = 0548, SL = 0000 },    // 0
                new CNT() { CK = 0569, DA = 0572, LN = 08, MD = 0568, SL = 0000 },    // 1
                new CNT() { CK = 0502, DA = 0505, LN = 08, MD = 0501, SL = 0517 },    // 2
                new CNT() { CK = 0519, DA = 0522, LN = 08, MD = 0518, SL = 0534 },    // 3
            };
            PAKs.SLG46120.acmp = new ACMP[]
            {
                new ACMP() { TH = 0596, HY = 0601, GN = 0603, LB = 0605 },    // 0
                new ACMP() { TH = 0607, HY = 0612, GN = 0614, LB = 0617 },    // 1
            };
            PAKs.SLG46120.RC_osc_freq = 25000;
            PAKs.SLG46120.RC_osc_freq_alt = 2000000;
            PAKs.SLG46120.RC_osc_src = 0536;
            PAKs.SLG46120.RC_osc_pre_div = 0537;
            PAKs.SLG46120.lock_status = 712;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46116
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46116()
        {
            PAKs.SLG46116 = new PAK();
            PAKs.SLG46116.base_die = "SLG46116";
            PAKs.SLG46116.package = "STQFN-14L_b";         //### this may change
            PAKs.SLG46116.package_size = "1.6mm x 2.5mm";
            PAKs.SLG46116.PAK_family = 3;
            PAKs.SLG46116.power_PAK = true;
            PAKs.SLG46116.package_weight = "0.0059 g";
            PAKs.SLG46116.pattern_id_address = 433;
            PAKs.SLG46116.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "GPI",     IM = 0379, RV = 0381,                                  VS = 1  },    // 01
                new PIN() { PT = "GPIO",    IO = 0383, RV = 0386, UD = 0388, DR = 0389,            VS = 1  },    // 02
                new PIN() { PT = "GPIO",    IO = 0390, RV = 0393, UD = 0395, DR = 0396,            VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", IM = 0397, OM = 0399, RV = 0401, UD = 0403, OE = 0015, VS = 1  },    // 04
                new PIN() { PT = "VIN",                                                                    },    // 05
                new PIN() { PT = "NA",                                                                     },    // 06
                new PIN() { PT = "VOUT",                                                                   },    // 07
                new PIN() { PT = "GND",                                                                    },    // 08
                new PIN() { PT = "GND",                                                                    },    // 09
                new PIN() { PT = "GPIO",    IO = 0404, RV = 0407, UD = 0409, DR = 0410,            VS = 1  },    // 10
                new PIN() { PT = "GPIO",    IO = 0411, RV = 0414, UD = 0416, DR = 0417,            VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", IM = 0418, OM = 0420, RV = 0422, UD = 0424, OE = 0210, VS = 1  },    // 12
                new PIN() { PT = "GPIO",    IO = 0425, RV = 0428, UD = 0430, DR = 0431,            VS = 1  },    // 13
                new PIN() { PT = "VDD"                                                                     },    // 14
            };
            PAKs.SLG46116.cnt = new CNT[]
            {
                new CNT() { CK = 0315, DA = 0318, LN = 14, MD = 0314, SL = 0000 },    // 0
                new CNT() { CK = 0329, DA = 0332, LN = 08, MD = 0328, SL = 0000 },    // 1
                new CNT() { CK = 0286, DA = 0289, LN = 08, MD = 0285, SL = 0301 },    // 2
                new CNT() { CK = 0343, DA = 0346, LN = 08, MD = 0342, SL = 0000 },    // 3
            };
            PAKs.SLG46116.RC_osc_freq = 25000;
            PAKs.SLG46116.RC_osc_freq_alt = 2000000;
            PAKs.SLG46116.RC_osc_src = 0303;
            PAKs.SLG46116.RC_osc_pre_div = 0304;
            PAKs.SLG46116.lock_status = 450;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46117
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46117()
        {
            PAKs.SLG46117 = new PAK();
            PAKs.SLG46117.base_die = "SLG46117";
            PAKs.SLG46117.package = "STQFN-14L_b";         //### this may change
            PAKs.SLG46117.package_size = "1.6mm x 2.5mm";
            PAKs.SLG46117.PAK_family = 3;
            PAKs.SLG46117.power_PAK = true;
            PAKs.SLG46117.package_weight = "0.0059 g";
            PAKs.SLG46117.pattern_id_address = 433;
            PAKs.SLG46117.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "GPI",     IM = 0379, RV = 0381,                                  VS = 1  },    // 01
                new PIN() { PT = "GPIO",    IO = 0383, RV = 0386, UD = 0388, DR = 0389,            VS = 1  },    // 02
                new PIN() { PT = "GPIO",    IO = 0390, RV = 0393, UD = 0395, DR = 0396,            VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", IM = 0397, OM = 0399, RV = 0401, UD = 0403, OE = 0015, VS = 1  },    // 04
                new PIN() { PT = "VIN",                                                                    },    // 05
                new PIN() { PT = "NA",                                                                     },    // 06
                new PIN() { PT = "VOUT",                                                                   },    // 07
                new PIN() { PT = "GND",                                                                    },    // 08
                new PIN() { PT = "GND",                                                                    },    // 09
                new PIN() { PT = "GPIO",    IO = 0404, RV = 0407, UD = 0409, DR = 0410,            VS = 1  },    // 10
                new PIN() { PT = "GPIO",    IO = 0411, RV = 0414, UD = 0416, DR = 0417,            VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", IM = 0418, OM = 0420, RV = 0422, UD = 0424, OE = 0210, VS = 1  },    // 12
                new PIN() { PT = "GPIO",    IO = 0425, RV = 0428, UD = 0430, DR = 0431,            VS = 1  },    // 13
                new PIN() { PT = "VDD"                                                                     },    // 14
            };
            PAKs.SLG46117.cnt = new CNT[]
            {
                new CNT() { CK = 0315, DA = 0318, LN = 14, MD = 0314, SL = 0000 },    // 0
                new CNT() { CK = 0329, DA = 0332, LN = 08, MD = 0328, SL = 0000 },    // 1
                new CNT() { CK = 0286, DA = 0289, LN = 08, MD = 0285, SL = 0301 },    // 2
                new CNT() { CK = 0343, DA = 0346, LN = 08, MD = 0342, SL = 0000 },    // 3
            };
            PAKs.SLG46117.RC_osc_freq = 25000;
            PAKs.SLG46117.RC_osc_freq_alt = 2000000;
            PAKs.SLG46117.RC_osc_src = 0303;
            PAKs.SLG46117.RC_osc_pre_div = 0304;
            PAKs.SLG46117.lock_status = 450;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46121
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46121()
        {
            PAKs.SLG46121 = new PAK();
            PAKs.SLG46121.base_die = "SLG46121";
            PAKs.SLG46121.package = "STQFN-12L";
            PAKs.SLG46121.package_size = "1.6mm x 1.6mm";
            PAKs.SLG46121.PAK_family = 3;
            PAKs.SLG46121.package_weight = "0.0038 g";
            PAKs.SLG46121.pattern_id_address = 691;
            PAKs.SLG46121.dual_supply_PAK = true;
            PAKs.SLG46121.dual_supply_vdd_pins = "PIN2-PIN6";
            PAKs.SLG46121.dual_supply_vdd2_pins = "PIN8-PIN12";
            PAKs.SLG46121.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "VDD",                                                                    },    // 01
                new PIN() { PT = "GPI",     IM = 0623, RV = 0625,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",    IO = 0627, RV = 0630, UD = 0632, DR = 0633,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",    IO = 0634, RV = 0637, UD = 0639, DR = 0640,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",    IO = 0641, RV = 0644, UD = 0646, DR = 0647,            VS = 1  },    // 05
                new PIN() { PT = "GPIO_OE", IM = 0648, OM = 0650, RV = 0652, UD = 0654, OE = 0024, VS = 1  },    // 06
                new PIN() { PT = "GND",                                                                    },    // 07
                new PIN() { PT = "GPIO",    IO = 0655, RV = 0658, UD = 0660, DR = 0661,            VS = 1  },    // 08
                new PIN() { PT = "GPIO",    IO = 0662, RV = 0665, UD = 0667, DR = 0668,            VS = 1  },    // 09
                new PIN() { PT = "GPIO_OE", IM = 0669, OM = 0671, RV = 0673, UD = 0675, OE = 0378, VS = 1  },    // 10
                new PIN() { PT = "VDD2",                                                                   },    // 11
                new PIN() { PT = "GPIO",    IO = 0683, RV = 0686, UD = 0688, DR = 0689,            VS = 1  },    // 12
            };
            PAKs.SLG46121.cnt = new CNT[]
            {
                new CNT() { CK = 0549, DA = 0552, LN = 14, MD = 0548, SL = 0000 },    // 0
                new CNT() { CK = 0569, DA = 0572, LN = 08, MD = 0568, SL = 0000 },    // 1
                new CNT() { CK = 0502, DA = 0505, LN = 08, MD = 0501, SL = 0517 },    // 2
                new CNT() { CK = 0519, DA = 0522, LN = 08, MD = 0518, SL = 0534 },    // 3
            };
            PAKs.SLG46121.acmp = new ACMP[]
            {
                new ACMP() { TH = 0596, HY = 0601, GN = 0603, LB = 0605 },    // 0
                new ACMP() { TH = 0607, HY = 0612, GN = 0614, LB = 0617 },    // 1
            };
            PAKs.SLG46121.RC_osc_freq = 25000;
            PAKs.SLG46121.RC_osc_freq_alt = 2000000;
            PAKs.SLG46121.RC_osc_src = 0536;
            PAKs.SLG46121.RC_osc_pre_div = 0537;
            PAKs.SLG46121.lock_status = 712;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46108
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46108()
        {
            PAKs.SLG46108 = new PAK();
            PAKs.SLG46108.base_die = "SLG46108";
            PAKs.SLG46108.package = "STQFN-8L";
            PAKs.SLG46108.package_size = "1mm x 1.2mm";
            PAKs.SLG46108.PAK_family = 3;
            PAKs.SLG46108.package_weight = "0.0017 g";
            PAKs.SLG46108.pattern_id_address = 0393;
            PAKs.SLG46108.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "VDD",                                                                    },    // 01
                new PIN() { PT = "GPI",     IM = 0349, RV = 0351,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",    IO = 0353, RV = 0356, UD = 0358, DR = 0359,            VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", IM = 0360, OM = 0362, RV = 0364, UD = 0366, OE = 0010, VS = 1  },    // 04
                new PIN() { PT = "GND",                                                                    },    // 05
                new PIN() { PT = "GPIO",    IO = 0368, RV = 0371, UD = 0373, DR = 0374,            VS = 1  },    // 06
                new PIN() { PT = "GPIO",    IO = 0375, RV = 0378, UD = 0380, DR = 0381,            VS = 1  },    // 07
                new PIN() { PT = "GPIO_OE", IM = 0382, OM = 0384, RV = 0386, UD = 0388, OE = 0195, VS = 1  },    // 08
            };
            PAKs.SLG46108.cnt = new CNT[]
            {
                new CNT() { CK = 0303, DA = 0306, LN = 08, MD = 0301, SL = 0001 },    // 0
                new CNT() { CK = 0320, DA = 0323, LN = 08, MD = 0318, SL = 0001 },    // 1
                new CNT() { CK = 0270, DA = 0273, LN = 08, MD = 0269, SL = 0285 },    // 2
                new CNT() { CK = 0336, DA = 0339, LN = 08, MD = 0335, SL = 0001 },    // 3
            };
            PAKs.SLG46108.acmp = new ACMP[]
            {
            };
            PAKs.SLG46108.RC_osc_freq = 25000;
            PAKs.SLG46108.RC_osc_freq_alt = 2000000;
            PAKs.SLG46108.RC_osc_src = 0287;
            PAKs.SLG46108.RC_osc_pre_div = 0288;
            PAKs.SLG46108.TM_note = "Note: The SN Code (Line 1 and Line 2) is generated during production, " +
                "and encodes information including part number, programming code number, date code and lot code." +
                "This same information is provided in plain text form on a label placed on the reel. If you " +
                "need assistance in decoding the SN Code, please contact Silego.";
            PAKs.SLG46108.lock_status = 402;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46169
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46169()
        {
            PAKs.SLG46169 = new PAK();
            PAKs.SLG46169.base_die = "SLG46169";
            PAKs.SLG46169.package = "STQFN-14L_c";
            PAKs.SLG46169.package_size = "2mm x 2.2mm";
            PAKs.SLG46169.PAK_family = 3;
            PAKs.SLG46169.package_weight = "0.00xx g";      // ###
            PAKs.SLG46169.pattern_id_address = 0970;
            PAKs.SLG46169.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                                },    // 00
                new PIN() { PT = "VDD",                                                                               },    // 01
                new PIN() { PT = "GPI",     RV = 0831,            IM = 0829,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",    RV = 0843, UD = 0845, IO = 0840, DR = 0846,                       VS = 1  },    // 03
                new PIN() { PT = "GPIO_OE", RV = 0851, UD = 0853, IM = 0847, OM = 0849, OE = 0024,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",    RV = 0857, UD = 0859, IO = 0854, DR = 0860,                       VS = 1  },    // 05
                new PIN() { PT = "GPIO_OE", RV = 0865, UD = 0867, IM = 0861, OM = 0863, OE = 0042,            VS = 1  },    // 06
                new PIN() { PT = "GPIO_OE", RV = 0879, UD = 0881, IM = 0875, OM = 0877, OE = 0060,            VS = 1  },    // 07
                new PIN() { PT = "SD",      RV = 0885, UD = 0887, IO = 0882, DR = 0888,            SD = 0889, VS = 1  },    // 08
                new PIN() { PT = "GND",                                                                               },    // 09
                new PIN() { PT = "GPIO_OE", RV = 0923, UD = 0925, IM = 0919, OM = 0921, OE = 0498,            VS = 1  },    // 10
                new PIN() { PT = "GPIO",    RV = 0929, UD = 0931, IO = 0926, DR = 0932,                       VS = 1  },    // 11
                new PIN() { PT = "GPIO_OE", RV = 0937, UD = 0939, IM = 0933, OM = 0935, OE = 0516,            VS = 1  },    // 12
                new PIN() { PT = "GPIO_OE", RV = 0944, UD = 0946, IM = 0940, OM = 0942, OE = 0528,            VS = 1  },    // 13
                new PIN() { PT = "GPIO",    RV = 0950, UD = 0952, IO = 0947, DR = 0953,                       VS = 1  },    // 14
            };
            PAKs.SLG46169.cnt = new CNT[]
            {
                new CNT() { CK = 0714, DA = 0717, LN = 14, MD = 0713, SL = 0000 },    // 0
                new CNT() { CK = 0734, DA = 0737, LN = 14, MD = 0733, SL = 0000 },    // 1
                new CNT() { CK = 0674, DA = 0677, LN = 08, MD = 0673, SL = 0705 },    // 2
                new CNT() { CK = 0690, DA = 0693, LN = 08, MD = 0689, SL = 0706 },    // 3
                new CNT() { CK = 0754, DA = 0757, LN = 08, MD = 0753, SL = 0000 },    // 4
                new CNT() { CK = 0768, DA = 0771, LN = 08, MD = 0767, SL = 0000 },    // 5
                new CNT() { CK = 0782, DA = 0785, LN = 08, MD = 0781, SL = 0000 },    // 6            
            };
            PAKs.SLG46169.acmp = new ACMP[]
            {
                new ACMP() { TH = 0795, HY = 0800, GN = 0802, LB = 0804 },  // 0
                new ACMP() { TH = 0806, HY = 0811, GN = 0813, LB = 0816 },  // 1
                new ACMP() { TH = 0818, HY = 0823, GN = 0825, LB = 0827 },  // 2
                new ACMP() { TH = 0552, HY = 0557, GN = 0559, LB = 0561 },  // 3
            };
            PAKs.SLG46169.RC_osc_freq = 25000;
            PAKs.SLG46169.RC_osc_freq_alt = 2000000;
            PAKs.SLG46169.RC_osc_src = 0708;
            PAKs.SLG46169.RC_osc_pre_div = 0565;
            PAKs.SLG46169.lock_status = 979;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        //  SLG46170
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static void createSLG46170()
        {
            PAKs.SLG46170 = new PAK();
            PAKs.SLG46170.base_die = "SLG46170";
            PAKs.SLG46170.package = "STQFN-14L_c";
            PAKs.SLG46170.package_size = "2mm x 2.2mm";
            PAKs.SLG46170.PAK_family = 3;
            PAKs.SLG46170.package_weight = "0.0xx g";   //###
            PAKs.SLG46170.pattern_id_address = 992;
            PAKs.SLG46170.pin = new PIN[]
            {
                new PIN() { PT = "NA",                                                                     },    // 00
                new PIN() { PT = "VDD",                                                                    },    // 01
                new PIN() { PT = "GPI",    IM = 0844,  RV = 0846,                                  VS = 1  },    // 02
                new PIN() { PT = "GPIO",   IO = 0855,  RV = 0889, UD = 0860, DR = 0861,            VS = 1  },    // 03
                new PIN() { PT = "GPIO",   IO = 0862,  RV = 0865, UD = 0867, DR = 0868,            VS = 1  },    // 04
                new PIN() { PT = "GPIO",   IO = 0869,  RV = 0872, UD = 0874, DR = 0875,            VS = 1  },    // 05
                new PIN() { PT = "GPIO",   IO = 0876,  RV = 0879, UD = 0881, DR = 0882,            VS = 1  },    // 06
                new PIN() { PT = "GPIO",   IO = 0890,  RV = 0893, UD = 0895, DR = 0896,            VS = 1  },    // 07
                new PIN() { PT = "SD",     IO = 0897,  RV = 0900, UD = 0902, DR = 0903, SD = 0904, VS = 1  },    // 08
                new PIN() { PT = "GND",                                                                    },    // 09
                new PIN() { PT = "GPIO",   IO = 0934,  RV = 0937, UD = 0939, DR = 0940,            VS = 1  },    // 10
                new PIN() { PT = "GPIO",   IO = 0941,  RV = 0944, UD = 0946, DR = 0947,            VS = 1  },    // 11
                new PIN() { PT = "GPIO",   IO = 0948,  RV = 0951, UD = 0953, DR = 0954,            VS = 1  },    // 12
                new PIN() { PT = "GPIO",   IO = 0955,  RV = 0958, UD = 0960, DR = 0961,            VS = 1  },    // 13
                new PIN() { PT = "GPIO",   IO = 0962,  RV = 0965, UD = 0967, DR = 0968,            VS = 1  },    // 14
            };
            PAKs.SLG46170.cnt = new CNT[]
            {
                new CNT() { CK = 0715, DA = 0717, LN = 14, MD = 0714, SL = 0000 },    // 0
                new CNT() { CK = 0735, DA = 0738, LN = 14, MD = 0734, SL = 0000 },    // 1
                new CNT() { CK = 0755, DA = 0758, LN = 08, MD = 0754, SL = 0000 },    // 2
                new CNT() { CK = 0769, DA = 0772, LN = 08, MD = 0768, SL = 0000 },    // 3
                new CNT() { CK = 0783, DA = 0786, LN = 08, MD = 0782, SL = 0000 },    // 4
                new CNT() { CK = 0797, DA = 0800, LN = 08, MD = 0796, SL = 0000 },    // 5
                new CNT() { CK = 0811, DA = 0814, LN = 08, MD = 0810, SL = 0000 },    // 6
                new CNT() { CK = 0825, DA = 0828, LN = 14, MD = 0824, SL = 0000 },    // 7
            };
            PAKs.SLG46170.acmp = new ACMP[]
            {
            };
            PAKs.SLG46170.RC_osc_freq = 25000;
            PAKs.SLG46170.RC_osc_freq_alt = 2000000;
            PAKs.SLG46170.RC_osc_src = 0970;
            PAKs.SLG46170.RC_osc_pre_div = 975;
            PAKs.SLG46170.lock_status = 979;
        }
    }

    public static class PAKs
    {
        // GreenPAK5

        public static PAK SLG46531;
        public static PAK SLG46532;
        public static PAK SLG46533;
        public static PAK SLG46534;
        public static PAK SLG46535;
        public static PAK SLG46536;
        public static PAK SLG50003;
        public static PAK SLG46533M;
        public static PAK SLG46537;
        public static PAK SLG46538;
        public static PAK SLG46537M;
        public static PAK SLG46538M;

        // GreenPAK4

        public static PAK SLG46140;
        public static PAK SLG46620;
        public static PAK SLG46621;

        // GreenPAK3

        public static PAK SLG46721;
        public static PAK SLG46722;
        public static PAK SLG46110;
        public static PAK SLG46120;
        public static PAK SLG46116;
        public static PAK SLG46117;
        public static PAK SLG46121;
        public static PAK SLG46108;
        public static PAK SLG46169;
        public static PAK SLG46170;
    }
}