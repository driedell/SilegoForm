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

        public bool dual_supply;
        public string dual_supply_vdd_pins;
        public string dual_supply_vdd2_pins;

        public mTM vdd;
        public mTM vdd2;
        public mTM temp;

        public struct mTM   // Min Typ Max
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
            public int address;
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
            public int MD;      // Counter mode (count/delay/etc)
            public int CK;      // Counter clock select
            public int DA;      // Counter Data
            public byte LN;     // Length (number of bits)
            public int SL;      // Selected (counter vs LUT)

            public int control;

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
            public int TH;      // Threshold selection
            public int HY;      // Hysteresis selection
            public int GN;      // Gain selection
            public int LB;      // Low Bandwidth Enable

            public bool used;
            public string acmpVIH;
            public string acmpVIL;
            public string low_bw;
            public string hysteresis;
        };

        public int LF_osc_freq;
        public int LF_osc_pre_div;
        public int RC_osc_freq;
        public int RC_osc_pre_div;
        public int RC_osc_src;
        public int RING_osc_freq;
        public int RING_osc_pre_div;

        // PAK5 stuff

        public int PAK5_nvm_read_lock = 1832;
        public int PAK5_nvm_write_lock_bank0_2 = 1871;
        public int PAK5_I2C_Slave_Address = 1864;
        public int PAK5_osc0 = 25000;
        public int PAK5_osc0_src = 1342;
        public int PAK5_osc0_pre_div = 1339;
        public int PAK5_osc0_force_on = 1343;
        public int PAK5_osc1 = 25000000;
        public int PAK5_osc1_pre_div = 1336;
        public int PAK5_osc1_force_on = 1341;

        // PAK4 stuff
        //### Verify this across all devices

        public int PAK4_LF_osc = 1743;
        public int PAK4_LF_osc_pre_div = 560;
        public int PAK4_RC_osc = 25000;
        public int PAK4_RC_osc_src = 565;
        public int PAK4_RC_osc_pre_div = 568;
        public int PAK4_RING_osc = 27250000;
        public int PAK4_RING_osc_pre_div = 576;

        // PAK3 stuff
        //### Verify this across all devices

        public int PAK3_RC_osc = 25000;
        public int PAK3_RC_osc_src = 708;
        public int PAK3_RC_osc_pre_div = 565;

        internal static void createPAKs(string Base_Die)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46531
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46531"))
            {
                PAKs.SLG46531 = new PAK();
                PAKs.SLG46531.base_die = "SLG46531";
                PAKs.SLG46531.package = "STQFN-20";
                PAKs.SLG46531.package_size = "2mm x 3mm";
                PAKs.SLG46531.PAK_family = 5;
                PAKs.SLG46531.package_weight = "0.0090 g";
                PAKs.SLG46531.pattern_id_address = 1840;
                PAKs.SLG46531.dual_supply = false;
                PAKs.SLG46531.pin = new PIN[] {
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
                    new CNT() { CK = 1314, MD = 1318, DA = 0xC5, LN = 16, SL = 1193 },    // 0
                    new CNT() { CK = 1322, MD = 1326, DA = 0xC7, LN = 16, SL = 1192 },    // 1
                    new CNT() { CK = 1274, MD = 1278, DA = 0xC0, LN = 8,  SL = 1198 },    // 2
                    new CNT() { CK = 1282, MD = 1286, DA = 0xC1, LN = 8,  SL = 1197 },    // 3
                    new CNT() { CK = 1290, MD = 1294, DA = 0xC2, LN = 8,  SL = 1196 },    // 4
                    new CNT() { CK = 1298, MD = 1302, DA = 0xC3, LN = 8,  SL = 1195 },    // 5
                    new CNT() { CK = 1306, MD = 1310, DA = 0xC4, LN = 8,  SL = 1194 },    // 6
                };
                PAKs.SLG46531.acmp = new ACMP[]
                {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                    new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
                };
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46532
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46532"))
            {
                PAKs.SLG46532 = new PAK();
                PAKs.SLG46532.base_die = "SLG46532";
                PAKs.SLG46532.package = "STQFN-20";
                PAKs.SLG46532.package_size = "2mm x 3mm";
                PAKs.SLG46532.PAK_family = 5;
                PAKs.SLG46532.package_weight = "0.0090 g";
                PAKs.SLG46532.pattern_id_address = 1840;
                PAKs.SLG46532.dual_supply = true;
                PAKs.SLG46532.dual_supply_vdd_pins = "PIN2-PIN10";
                PAKs.SLG46532.dual_supply_vdd2_pins = "PIN12-PIN20";
                PAKs.SLG46532.pin = new PIN[] {
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
                    new CNT() { CK = 1314, MD = 1318, DA = 0xC5, LN = 16, SL = 1193 },    // 0
                    new CNT() { CK = 1322, MD = 1326, DA = 0xC7, LN = 16, SL = 1192 },    // 1
                    new CNT() { CK = 1274, MD = 1278, DA = 0xC0, LN = 8,  SL = 1198 },    // 2
                    new CNT() { CK = 1282, MD = 1286, DA = 0xC1, LN = 8,  SL = 1197 },    // 3
                    new CNT() { CK = 1290, MD = 1294, DA = 0xC2, LN = 8,  SL = 1196 },    // 4
                    new CNT() { CK = 1298, MD = 1302, DA = 0xC3, LN = 8,  SL = 1195 },    // 5
                    new CNT() { CK = 1306, MD = 1310, DA = 0xC4, LN = 8,  SL = 1194 },    // 6
                };
                PAKs.SLG46532.acmp = new ACMP[]
                {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                    new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
                };
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46533
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46533"))
            {
                PAKs.SLG46533 = new PAK();
                PAKs.SLG46533.base_die = "SLG46533";
                PAKs.SLG46533.package = "STQFN-20";
                PAKs.SLG46533.package_size = "2mm x 3mm";
                PAKs.SLG46533.PAK_family = 5;
                PAKs.SLG46533.package_weight = "0.0090 g";
                PAKs.SLG46533.pattern_id_address = 1840;
                PAKs.SLG46533.dual_supply = false;
                PAKs.SLG46533.pin = new PIN[] {
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
                    new CNT() { CK = 1314, MD = 1318, DA = 0xC5, LN = 16, SL = 1193 },    // 0
                    new CNT() { CK = 1322, MD = 1326, DA = 0xC7, LN = 16, SL = 1192 },    // 1
                    new CNT() { CK = 1274, MD = 1278, DA = 0xC0, LN = 8,  SL = 1198 },    // 2
                    new CNT() { CK = 1282, MD = 1286, DA = 0xC1, LN = 8,  SL = 1197 },    // 3
                    new CNT() { CK = 1290, MD = 1294, DA = 0xC2, LN = 8,  SL = 1196 },    // 4
                    new CNT() { CK = 1298, MD = 1302, DA = 0xC3, LN = 8,  SL = 1195 },    // 5
                    new CNT() { CK = 1306, MD = 1310, DA = 0xC4, LN = 8,  SL = 1194 },    // 6
                };
                PAKs.SLG46533.acmp = new ACMP[]
                {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                    new ACMP() { TH = 1648, GN = 1653, LB = 1655, HY = 1178 },    // 3
                };
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46534
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46534"))
            {
                PAKs.SLG46534 = new PAK();
                PAKs.SLG46534.base_die = "SLG46534";
                PAKs.SLG46534.package = "STQFN-14";
                PAKs.SLG46534.package_size = "2mm x 2.2mm";
                PAKs.SLG46534.PAK_family = 5;
                PAKs.SLG46534.package_weight = "0.0067 g";
                PAKs.SLG46534.dual_supply = false;
                PAKs.SLG46534.pattern_id_address = 1840;
                PAKs.SLG46534.pin = new PIN[] {
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
                    new CNT() { CK = 1314, MD = 1318, DA = 0xC5, LN = 16, SL = 1193 },    // 0
                    new CNT() { CK = 1322, MD = 1326, DA = 0xC7, LN = 16, SL = 1192 },    // 1
                    new CNT() { CK = 1274, MD = 1278, DA = 0xC0, LN = 8,  SL = 1198 },    // 2
                    new CNT() { CK = 1282, MD = 1286, DA = 0xC1, LN = 8,  SL = 1197 },    // 3
                    new CNT() { CK = 1290, MD = 1294, DA = 0xC2, LN = 8,  SL = 1196 },    // 4
                    new CNT() { CK = 1298, MD = 1302, DA = 0xC3, LN = 8,  SL = 1195 },    // 5
                    new CNT() { CK = 1306, MD = 1310, DA = 0xC4, LN = 8,  SL = 1194 },    // 6
                };
                PAKs.SLG46534.acmp = new ACMP[]
                {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                };
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46535
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46535"))
            {
                PAKs.SLG46535 = new PAK();
                PAKs.SLG46535.base_die = "SLG46535";
                PAKs.SLG46535.package = "STQFN-14";
                PAKs.SLG46535.package_size = "2mm x 2.2mm";
                PAKs.SLG46535.PAK_family = 5;
                PAKs.SLG46535.package_weight = "0.0068 g";
                PAKs.SLG46535.dual_supply = true;
                PAKs.SLG46535.dual_supply_vdd_pins = "PIN2-PIN8";
                PAKs.SLG46535.dual_supply_vdd2_pins = "PIN10-PIN14";
                PAKs.SLG46535.pattern_id_address = 1840;
                PAKs.SLG46535.pin = new PIN[] {
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
                    new CNT() { CK = 1314, MD = 1318, DA = 0xC5, LN = 16, SL = 1193 },    // 0
                    new CNT() { CK = 1322, MD = 1326, DA = 0xC7, LN = 16, SL = 1192 },    // 1
                    new CNT() { CK = 1274, MD = 1278, DA = 0xC0, LN = 8,  SL = 1198 },    // 2
                    new CNT() { CK = 1282, MD = 1286, DA = 0xC1, LN = 8,  SL = 1197 },    // 3
                    new CNT() { CK = 1290, MD = 1294, DA = 0xC2, LN = 8,  SL = 1196 },    // 4
                    new CNT() { CK = 1298, MD = 1302, DA = 0xC3, LN = 8,  SL = 1195 },    // 5
                    new CNT() { CK = 1306, MD = 1310, DA = 0xC4, LN = 8,  SL = 1194 },    // 6
                };
                PAKs.SLG46535.acmp = new ACMP[]
                {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                };
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46536
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46536"))
            {
                PAKs.SLG46536 = new PAK();
                PAKs.SLG46536.base_die = "SLG46536";
                PAKs.SLG46536.package = "STQFN-14";
                PAKs.SLG46536.package_size = "2mm x 2.2mm";
                PAKs.SLG46536.PAK_family = 5;
                PAKs.SLG46536.package_weight = "0.0066 g";
                PAKs.SLG46536.dual_supply = false;
                PAKs.SLG46536.pattern_id_address = 1840;
                PAKs.SLG46536.pin = new PIN[] {
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
                    new CNT() { CK = 1314, MD = 1318, DA = 0xC5, LN = 16, SL = 1193 },    // 0
                    new CNT() { CK = 1322, MD = 1326, DA = 0xC7, LN = 16, SL = 1192 },    // 1
                    new CNT() { CK = 1274, MD = 1278, DA = 0xC0, LN = 8,  SL = 1198 },    // 2
                    new CNT() { CK = 1282, MD = 1286, DA = 0xC1, LN = 8,  SL = 1197 },    // 3
                    new CNT() { CK = 1290, MD = 1294, DA = 0xC2, LN = 8,  SL = 1196 },    // 4
                    new CNT() { CK = 1298, MD = 1302, DA = 0xC3, LN = 8,  SL = 1195 },    // 5
                    new CNT() { CK = 1306, MD = 1310, DA = 0xC4, LN = 8,  SL = 1194 },    // 6
                };
                PAKs.SLG46536.acmp = new ACMP[]
                {
                    new ACMP() { TH = 1624, GN = 1629, LB = 1629, HY = 1174 },    // 0
                    new ACMP() { TH = 1632, GN = 1637, LB = 1639, HY = 1170 },    // 1
                    new ACMP() { TH = 1640, GN = 1645, LB = 1647, HY = 1181 },    // 2
                };
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46140
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46140"))
            {
                PAKs.SLG46140 = new PAK();
                PAKs.SLG46140.base_die = "SLG46140";
                PAKs.SLG46140.package = "STQFN-14";
                PAKs.SLG46140.package_size = "1.6mm x 2mm";
                PAKs.SLG46140.PAK_family = 4;
                PAKs.SLG46140.package_weight = "0.0090 g";  //###
                PAKs.SLG46140.pattern_id_address = 1007;
                PAKs.SLG46140.dual_supply = false;
                PAKs.SLG46140.pin = new PIN[] {
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
                new CNT() { CK = 0737, MD = 0743, DA = 0722, LN = 16, SL = 0743 },    // 0
                new CNT() { CK = 0714, MD = 0720, DA = 0705, LN = 08, SL = 0720 },    // 0
                new CNT() { CK = 0695, MD = 0701, DA = 0680, LN = 08, SL = 0701 },    // 0
                new CNT() { CK = 0670, MD = 0676, DA = 0661, LN = 08, SL = 0676 },    // 0

                // new CNT() { control = 737, DA = 722, LN = 14, SL = 743 },    // 0
                // new CNT() { control = 714, DA = 705, LN = 8,  SL = 720 },    // 1
                // new CNT() { control = 695, DA = 680, LN = 8,  SL = 701 },    // 2
                // new CNT() { control = 670, DA = 661, LN = 8,  SL = 676 },    // 3
                };
                PAKs.SLG46140.acmp = new ACMP[]
                {
                new ACMP() { TH = 496, HY = 510, GN = 522, LB = 524 },    // 0
                new ACMP() { TH = 501, HY = 508, GN = 519, LB = 518 },    // 1
                };
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46620
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46620"))
            {
                PAKs.SLG46620 = new PAK();
                PAKs.SLG46620.base_die = "SLG46620";
                PAKs.SLG46620.package = "STQFN-20";
                PAKs.SLG46620.package_size = "2mm x 3mm";
                PAKs.SLG46620.PAK_family = 4;
                PAKs.SLG46620.package_weight = "0.015 g";
                PAKs.SLG46620.pattern_id_address = 2031;
                PAKs.SLG46620.dual_supply = false;
                PAKs.SLG46620.pin = new PIN[] {
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
                    new CNT() { CK = 1745, MD = 1750, DA = 1731, LN = 14, SL = 1750 },    // 0
                    new CNT() { CK = 1767, MD = 1772, DA = 1753, LN = 14, SL = 1772 },    // 1
                    new CNT() { CK = 1788, MD = 1794, DA = 1774, LN = 14, SL = 1794 },    // 2
                    new CNT() { CK = 1813, MD = 1818, DA = 1799, LN = 14, SL = 1818 },    // 3
                    new CNT() { CK = 1828, MD = 1834, DA = 1820, LN = 08, SL = 1834 },    // 4
                    new CNT() { CK = 1846, MD = 1851, DA = 1838, LN = 08, SL = 1851 },    // 5
                    new CNT() { CK = 1860, MD = 1865, DA = 1852, LN = 08, SL = 1865 },    // 6
                    new CNT() { CK = 1874, MD = 1879, DA = 1866, LN = 08, SL = 1879 },    // 7
                    new CNT() { CK = 1888, MD = 1894, DA = 1880, LN = 08, SL = 1894 },    // 8
                    new CNT() { CK = 1903, MD = 1909, DA = 1895, LN = 08, SL = 1909 },    // 9

                    //new CNT() { control = 1745, DA = 1731, LN = 14, SL = 1750 },    // 0
                    //new CNT() { control = 1767, DA = 1753, LN = 14, SL = 1772 },    // 1
                    //new CNT() { control = 1788, DA = 1774, LN = 14, SL = 1794 },    // 2
                    //new CNT() { control = 1813, DA = 1799, LN = 14, SL = 1818 },    // 3
                    //new CNT() { control = 1828, DA = 1820, LN = 08, SL = 1834 },    // 4
                    //new CNT() { control = 1846, DA = 1838, LN = 08, SL = 1851 },    // 5
                    //new CNT() { control = 1860, DA = 1852, LN = 08, SL = 1865 },    // 6
                    //new CNT() { control = 1874, DA = 1866, LN = 08, SL = 1879 },    // 7
                    //new CNT() { control = 1888, DA = 1880, LN = 08, SL = 1894 },    // 8
                    //new CNT() { control = 1903, DA = 1895, LN = 08, SL = 1909 },    // 9
                };
                PAKs.SLG46620.acmp = new ACMP[]
                {
                    new ACMP() { TH = 892, HY = 934, GN = 853, LB = 852 },  // 0
                    new ACMP() { TH = 897, HY = 932, GN = 857, LB = 861 },  // 1
                    new ACMP() { TH = 902, HY = 930, GN = 864, LB = 862 },  // 2
                    new ACMP() { TH = 907, HY = 928, GN = 867, LB = 866 },  // 3
                    new ACMP() { TH = 912, HY = 926, GN = 871, LB = 875 },  // 4
                    new ACMP() { TH = 917, HY = 924, GN = 871, LB = 880 },  // 5    //### ACMP5 has no gain option, fix this
                };
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46621
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46621"))
            {
                PAKs.SLG46621 = new PAK();
                PAKs.SLG46621.base_die = "SLG46621";
                PAKs.SLG46621.package = "STQFN-20";
                PAKs.SLG46621.package_size = "2mm x 3mm";
                PAKs.SLG46621.PAK_family = 4;
                PAKs.SLG46621.package_weight = "0.015 g";
                PAKs.SLG46621.pattern_id_address = 2031;
                PAKs.SLG46621.dual_supply = true;
                PAKs.SLG46621.dual_supply_vdd_pins = "PIN2-PIN10";
                PAKs.SLG46621.dual_supply_vdd2_pins = "PIN12-PIN20";
                PAKs.SLG46620.pin = new PIN[] {
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
                    new CNT() { control = 1745, DA = 1731, LN = 14, SL = 1750 },    // 0
                    new CNT() { control = 1767, DA = 1753, LN = 14, SL = 1772 },    // 1
                    new CNT() { control = 1788, DA = 1774, LN = 14, SL = 1794 },    // 2
                    new CNT() { control = 1813, DA = 1799, LN = 14, SL = 1818 },    // 3
                    new CNT() { control = 1828, DA = 1820, LN = 08, SL = 1834 },    // 4
                    new CNT() { control = 1846, DA = 1838, LN = 08, SL = 1851 },    // 5
                    new CNT() { control = 1860, DA = 1852, LN = 08, SL = 1865 },    // 6
                    new CNT() { control = 1874, DA = 1866, LN = 08, SL = 1879 },    // 7
                    new CNT() { control = 1888, DA = 1880, LN = 08, SL = 1894 },    // 8
                    new CNT() { control = 1903, DA = 1895, LN = 08, SL = 1909 },    // 9
                };
                PAKs.SLG46621.acmp = new ACMP[]
                {
                    new ACMP() { TH = 892, HY = 934, GN = 853, LB = 852 },  // 0
                    new ACMP() { TH = 897, HY = 932, GN = 857, LB = 861 },  // 1
                    new ACMP() { TH = 902, HY = 930, GN = 864, LB = 862 },  // 2
                    new ACMP() { TH = 907, HY = 928, GN = 867, LB = 866 },  // 3
                    new ACMP() { TH = 912, HY = 926, GN = 871, LB = 875 },  // 4
                    new ACMP() { TH = 917, HY = 924, GN = 871, LB = 880 },  // 5    //### ACMP5 has no gain option, fix this
                };
            }
            
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46721
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46721"))
            {
                PAKs.SLG46721 = new PAK();
                PAKs.SLG46721.base_die = "SLG46721";
                PAKs.SLG46721.package = "STQFN-20";
                PAKs.SLG46721.package_size = "2mm x 3mm";
                PAKs.SLG46721.PAK_family = 3;
                PAKs.SLG46721.package_weight = "0.015 g";
                PAKs.SLG46721.pattern_id_address = 970; // ### check theProgram to make sure i updated this
                PAKs.SLG46721.dual_supply = false;
                PAKs.SLG46721.pin = new PIN[] {
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
                    new CNT() { control = 0713, DA = 0717, LN = 14, SL = 0000 },    // 0
                    new CNT() { control = 0733, DA = 0737, LN = 14, SL = 0000 },    // 1
                    new CNT() { control = 0000, DA = 0677, LN = 08, SL = 0705 },    // 2
                    new CNT() { control = 0000, DA = 0693, LN = 08, SL = 0706 },    // 3
                    new CNT() { control = 0753, DA = 0757, LN = 08, SL = 0000 },    // 4
                    new CNT() { control = 0767, DA = 0771, LN = 08, SL = 0000 },    // 5
                    new CNT() { control = 0781, DA = 0785, LN = 08, SL = 0000 },    // 6
                };
                PAKs.SLG46721.acmp = new ACMP[]
                {
                    new ACMP() { TH = 0795, HY = 0800, GN = 0802, LB = 0804 },  // 0
                    new ACMP() { TH = 0806, HY = 0811, GN = 0813, LB = 0816 },  // 1
                    new ACMP() { TH = 0818, HY = 0823, GN = 0825, LB = 0827 },  // 2
                    new ACMP() { TH = 0552, HY = 0557, GN = 0559, LB = 0561 },  // 3
                };
            }

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46722
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Base_Die.Equals("SLG46722"))
            {
                PAKs.SLG46722 = new PAK();
                PAKs.SLG46722.base_die = "SLG46722";
                PAKs.SLG46722.package = "STQFN-20";
                PAKs.SLG46722.package_size = "2mm x 3mm";
                PAKs.SLG46722.PAK_family = 3;
                PAKs.SLG46722.package_weight = "0.015 g";
                PAKs.SLG46722.pattern_id_address = 992; // ### check theProgram to make sure i updated this
                PAKs.SLG46722.dual_supply = false;
                PAKs.SLG46722.pin = new PIN[] {
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
                    new CNT() { control = 0713, DA = 0717, LN = 14, SL = 0001 },    // 0
                    new CNT() { control = 0733, DA = 0737, LN = 14, SL = 0001 },    // 1
                    new CNT() { control = 0000, DA = 0677, LN = 08, SL = 0705 },    // 2
                    new CNT() { control = 0000, DA = 0693, LN = 08, SL = 0706 },    // 3
                    new CNT() { control = 0753, DA = 0757, LN = 08, SL = 0001 },    // 4
                    new CNT() { control = 0767, DA = 0771, LN = 08, SL = 0001 },    // 5
                    new CNT() { control = 0781, DA = 0785, LN = 08, SL = 0001 },    // 6
                };
                PAKs.SLG46722.acmp = new ACMP[]
                {
                    new ACMP() { TH = 0795, HY = 0800, GN = 0802, LB = 0804 },  // 0
                    new ACMP() { TH = 0806, HY = 0811, GN = 0813, LB = 0816 },  // 1
                    new ACMP() { TH = 0818, HY = 0823, GN = 0825, LB = 0827 },  // 2
                    new ACMP() { TH = 0552, HY = 0557, GN = 0559, LB = 0561 },  // 3
                };
            }
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
    }
}