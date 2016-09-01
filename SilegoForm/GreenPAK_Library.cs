namespace GreenPAK_Library
{
    public class PAK
    {
        public string base_die;
        public string package;
        public string package_size;
        public byte PAK_family;
        public string package_weight;
        public byte pattern_id_address;

        public bool dual_supply;
        public string dual_supply_vdd_pins;
        public string dual_supply_vdd2_pins;
        public int vdd2_pin;
        public byte gnd_pin;

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
            public byte address;
            public string pin_type;
            public byte OE;
            public byte vdd_src;
            public string name;
            public string type;
            public string resistor;
            public string description;
        };

        public CNT[] cnt;

        public struct CNT
        {
            public byte control;
            public byte data;
            public byte length;
            public int selected;
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
            public bool used;
            public byte control;
            public int hyst;
            public string acmpVIH;
            public string acmpVIL;
            public string low_bw;
            public string hysteresis;
        };

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

        // ### PAK4 stuff

        // ### PAK3 stuff

        internal static void createPAKs()
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46531
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46531 = new PAK();
            PAKs.SLG46531.base_die = "SLG46531";
            PAKs.SLG46531.package = "STQFN-20";
            PAKs.SLG46531.package_size = "2mm x 3mm";
            PAKs.SLG46531.PAK_family = 5;
            PAKs.SLG46531.package_weight = "0.0090 g";
            PAKs.SLG46531.pattern_id_address = 0xE6;
            PAKs.SLG46531.dual_supply = false;
            PAKs.SLG46531.gnd_pin = 11;
            PAKs.SLG46531.pin = new PIN[] {
                new PIN() { address = 0x00, pin_type = "NA",                 vdd_src = 0  },        // 0
                new PIN() { address = 0x00, pin_type = "VDD",                vdd_src = 0  },        // 1
                new PIN() { address = 0x80, pin_type = "GPI",                vdd_src = 1  },        // 2
                new PIN() { address = 0x81, pin_type = "GPIO_OE", OE = 0x1A, vdd_src = 1  },        // 3
                new PIN() { address = 0x82, pin_type = "GPIO",               vdd_src = 1  },        // 4
                new PIN() { address = 0x83, pin_type = "GPIO_OE", OE = 0x1D, vdd_src = 1  },        // 5
                new PIN() { address = 0x84, pin_type = "GPIO",               vdd_src = 1  },        // 6
                new PIN() { address = 0x85, pin_type = "GPIO_OE", OE = 0x20, vdd_src = 1  },        // 7
                new PIN() { address = 0x86, pin_type = "I2C",                vdd_src = 1  },        // 8
                new PIN() { address = 0x87, pin_type = "I2C",                vdd_src = 1  },        // 9
                new PIN() { address = 0x88, pin_type = "SD_OE",   OE = 0x24, vdd_src = 1  },        // 10
                new PIN() { address = 0x00, pin_type = "GND",                vdd_src = 0  },        // 11
                new PIN() { address = 0x89, pin_type = "SD",                 vdd_src = 1  },        // 12
                new PIN() { address = 0x8A, pin_type = "GPIO_OE", OE = 0x27, vdd_src = 1  },        // 13
                new PIN() { address = 0x8B, pin_type = "GPIO_OE", OE = 0x29, vdd_src = 1  },        // 14
                new PIN() { address = 0x8C, pin_type = "GPIO",               vdd_src = 1  },        // 15
                new PIN() { address = 0x8D, pin_type = "GPIO_OE", OE = 0x2C, vdd_src = 1  },        // 16
                new PIN() { address = 0x8E, pin_type = "GPIO",               vdd_src = 1  },        // 17
                new PIN() { address = 0x8F, pin_type = "GPIO_OE", OE = 0x2F, vdd_src = 1  },        // 18
                new PIN() { address = 0x90, pin_type = "GPIO_OE", OE = 0x31, vdd_src = 1  },        // 19
                new PIN() { address = 0x91, pin_type = "GPIO",               vdd_src = 1  },        // 20
            };
            PAKs.SLG46531.cnt = new CNT[]
            {
                new CNT() { control = 0xA4, data = 0xC5, length = 16, selected = 1193 },    // 0
                new CNT() { control = 0xA5, data = 0xC7, length = 16, selected = 1192 },    // 1
                new CNT() { control = 0x9F, data = 0xC0, length = 8,  selected = 1198 },    // 2
                new CNT() { control = 0xA0, data = 0xC1, length = 8,  selected = 1197 },    // 3
                new CNT() { control = 0xA1, data = 0xC2, length = 8,  selected = 1196 },    // 4
                new CNT() { control = 0xA2, data = 0xC3, length = 8,  selected = 1195 },    // 5
                new CNT() { control = 0xA3, data = 0xC4, length = 8,  selected = 1194 },    // 6
            };
            PAKs.SLG46531.acmp = new ACMP[]
            {
                new ACMP() { control = 0xCB, hyst = 1174 },
                new ACMP() { control = 0xCC, hyst = 1170 },
                new ACMP() { control = 0xCD, hyst = 1181 },
                new ACMP() { control = 0xCE, hyst = 1178 },
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46532
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46532 = new PAK();
            PAKs.SLG46532.base_die = "SLG46532";
            PAKs.SLG46532.package = "STQFN-20";
            PAKs.SLG46532.package_size = "2mm x 3mm";
            PAKs.SLG46532.PAK_family = 5;
            PAKs.SLG46532.package_weight = "0.0090 g";
            PAKs.SLG46532.pattern_id_address = 0xE6;
            PAKs.SLG46532.dual_supply = true;
            PAKs.SLG46532.dual_supply_vdd_pins = "PIN2-PIN10";
            PAKs.SLG46532.dual_supply_vdd2_pins = "PIN12-PIN20";
            PAKs.SLG46532.vdd2_pin = 14;
            PAKs.SLG46532.gnd_pin = 11;
            PAKs.SLG46532.pin = new PIN[] {
                new PIN() { address = 0x00, pin_type = "NA",      vdd_src = 0  },        // 0
                new PIN() { address = 0x00, pin_type = "VDD",     vdd_src = 0  },        // 1
                new PIN() { address = 0x80, pin_type = "GPI",     vdd_src = 1  },        // 2
                new PIN() { address = 0x81, pin_type = "GPIO_OE", vdd_src = 1  },        // 3
                new PIN() { address = 0x82, pin_type = "GPIO",    vdd_src = 1  },        // 4
                new PIN() { address = 0x83, pin_type = "GPIO_OE", vdd_src = 1  },        // 5
                new PIN() { address = 0x84, pin_type = "GPIO",    vdd_src = 1  },        // 6
                new PIN() { address = 0x85, pin_type = "GPIO_OE", vdd_src = 1  },        // 7
                new PIN() { address = 0x86, pin_type = "I2C",     vdd_src = 1  },        // 8
                new PIN() { address = 0x87, pin_type = "I2C",     vdd_src = 1  },        // 9
                new PIN() { address = 0x88, pin_type = "SD_OE",   vdd_src = 1  },        // 10
                new PIN() { address = 0x00, pin_type = "GND",     vdd_src = 0  },        // 11
                new PIN() { address = 0x89, pin_type = "SD",      vdd_src = 2  },        // 12
                new PIN() { address = 0x8A, pin_type = "GPIO_OE", vdd_src = 2  },        // 13
                new PIN() { address = 0x00, pin_type = "VDD2",    vdd_src = 0  },        // 14
                new PIN() { address = 0x8C, pin_type = "GPIO",    vdd_src = 2  },        // 15
                new PIN() { address = 0x8D, pin_type = "GPIO_OE", vdd_src = 2  },        // 16
                new PIN() { address = 0x8E, pin_type = "GPIO",    vdd_src = 2  },        // 17
                new PIN() { address = 0x8F, pin_type = "GPIO_OE", vdd_src = 2  },        // 18
                new PIN() { address = 0x90, pin_type = "GPIO_OE", vdd_src = 2  },        // 19
                new PIN() { address = 0x91, pin_type = "GPIO",    vdd_src = 2  },        // 20
            };
            PAKs.SLG46532.cnt = new CNT[]
            {
                new CNT() { control = 0xA4, data = 0xC5, length = 16, selected = 1193 },    // 0
                new CNT() { control = 0xA5, data = 0xC7, length = 16, selected = 1192 },    // 1
                new CNT() { control = 0x9F, data = 0xC0, length = 8,  selected = 1198 },    // 2
                new CNT() { control = 0xA0, data = 0xC1, length = 8,  selected = 1197 },    // 3
                new CNT() { control = 0xA1, data = 0xC2, length = 8,  selected = 1196 },    // 4
                new CNT() { control = 0xA2, data = 0xC3, length = 8,  selected = 1195 },    // 5
                new CNT() { control = 0xA3, data = 0xC4, length = 8,  selected = 1194 },    // 6
            };
            PAKs.SLG46532.acmp = new ACMP[]
            {
                new ACMP() { control = 0xCB, hyst = 1174 },
                new ACMP() { control = 0xCC, hyst = 1170 },
                new ACMP() { control = 0xCD, hyst = 1181 },
                new ACMP() { control = 0xCE, hyst = 1178 },
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46533
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46533 = new PAK();
            PAKs.SLG46533.base_die = "SLG46533";
            PAKs.SLG46533.package = "STQFN-20";
            PAKs.SLG46533.package_size = "2mm x 3mm";
            PAKs.SLG46533.PAK_family = 5;
            PAKs.SLG46533.package_weight = "0.0090 g";
            PAKs.SLG46533.pattern_id_address = 0xE6;
            PAKs.SLG46533.dual_supply = false;
            PAKs.SLG46533.gnd_pin = 11;
            PAKs.SLG46533.pin = new PIN[] {
                new PIN() { address = 0x00, pin_type = "NA",      vdd_src = 0  },        // 0
                new PIN() { address = 0x00, pin_type = "VDD",     vdd_src = 0  },        // 1
                new PIN() { address = 0x80, pin_type = "GPI",     vdd_src = 1  },        // 2
                new PIN() { address = 0x81, pin_type = "GPIO_OE", vdd_src = 1  },        // 3
                new PIN() { address = 0x82, pin_type = "GPIO",    vdd_src = 1  },        // 4
                new PIN() { address = 0x83, pin_type = "GPIO_OE", vdd_src = 1  },        // 5
                new PIN() { address = 0x84, pin_type = "GPIO",    vdd_src = 1  },        // 6
                new PIN() { address = 0x85, pin_type = "GPIO_OE", vdd_src = 1  },        // 7
                new PIN() { address = 0x86, pin_type = "I2C",     vdd_src = 1  },        // 8
                new PIN() { address = 0x87, pin_type = "I2C",     vdd_src = 1  },        // 9
                new PIN() { address = 0x88, pin_type = "SD_OE",   vdd_src = 1  },        // 10
                new PIN() { address = 0x00, pin_type = "GND",     vdd_src = 0  },        // 11
                new PIN() { address = 0x89, pin_type = "SD",      vdd_src = 1  },        // 12
                new PIN() { address = 0x8A, pin_type = "GPIO_OE", vdd_src = 1  },        // 13
                new PIN() { address = 0x0B, pin_type = "GPIO_OE", vdd_src = 1  },        // 14
                new PIN() { address = 0x8C, pin_type = "GPIO",    vdd_src = 1  },        // 15
                new PIN() { address = 0x8D, pin_type = "GPIO_OE", vdd_src = 1  },        // 16
                new PIN() { address = 0x8E, pin_type = "GPIO",    vdd_src = 1  },        // 17
                new PIN() { address = 0x8F, pin_type = "GPIO_OE", vdd_src = 1  },        // 18
                new PIN() { address = 0x90, pin_type = "GPIO_OE", vdd_src = 1  },        // 19
                new PIN() { address = 0x91, pin_type = "GPIO",    vdd_src = 1  },        // 20
            };
            PAKs.SLG46533.cnt = new CNT[]
            {
                new CNT() { control = 0xA4, data = 0xC5, length = 16, selected = 1193 },    // 0
                new CNT() { control = 0xA5, data = 0xC7, length = 16, selected = 1192 },    // 1
                new CNT() { control = 0x9F, data = 0xC0, length = 8,  selected = 1198 },    // 2
                new CNT() { control = 0xA0, data = 0xC1, length = 8,  selected = 1197 },    // 3
                new CNT() { control = 0xA1, data = 0xC2, length = 8,  selected = 1196 },    // 4
                new CNT() { control = 0xA2, data = 0xC3, length = 8,  selected = 1195 },    // 5
                new CNT() { control = 0xA3, data = 0xC4, length = 8,  selected = 1194 },    // 6
            };
            PAKs.SLG46533.acmp = new ACMP[]
            {
                new ACMP() { control = 0xCB, hyst = 1174 },
                new ACMP() { control = 0xCC, hyst = 1170 },
                new ACMP() { control = 0xCD, hyst = 1181 },
                new ACMP() { control = 0xCE, hyst = 1178 },
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46534
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46534 = new PAK();
            PAKs.SLG46534.base_die = "SLG46534";
            PAKs.SLG46534.package = "STQFN-14";
            PAKs.SLG46534.package_size = "2mm x 2.2mm";
            PAKs.SLG46534.PAK_family = 5;
            PAKs.SLG46534.package_weight = "0.0067 g";
            PAKs.SLG46534.dual_supply = false;
            PAKs.SLG46534.pattern_id_address = 0xE6;
            PAKs.SLG46534.gnd_pin = 9;
            PAKs.SLG46534.pin = new PIN[] {
                new PIN() { address = 0x00, pin_type = "NA",      vdd_src = 0  },        // 0
                new PIN() { address = 0x00, pin_type = "VDD",     vdd_src = 0  },        // 1
                new PIN() { address = 0x80, pin_type = "GPI",     vdd_src = 1  },        // 2
                new PIN() { address = 0x82, pin_type = "GPIO",    vdd_src = 1  },        // 3
                new PIN() { address = 0x84, pin_type = "GPIO",    vdd_src = 1  },        // 4
                new PIN() { address = 0x85, pin_type = "GPIO_OE", vdd_src = 1  },        // 5
                new PIN() { address = 0x86, pin_type = "I2C",     vdd_src = 1  },        // 6
                new PIN() { address = 0x87, pin_type = "I2C",     vdd_src = 1  },        // 7
                new PIN() { address = 0x88, pin_type = "SD_OE",   vdd_src = 1  },        // 8
                new PIN() { address = 0x00, pin_type = "GND",     vdd_src = 0  },        // 9
                new PIN() { address = 0x89, pin_type = "SD",      vdd_src = 1  },        // 10
                new PIN() { address = 0x8B, pin_type = "GPIO_OE", vdd_src = 1  },        // 11
                new PIN() { address = 0x8D, pin_type = "GPIO_OE", vdd_src = 1  },        // 12
                new PIN() { address = 0x8E, pin_type = "GPIO",    vdd_src = 1  },        // 13
                new PIN() { address = 0x91, pin_type = "GPIO",    vdd_src = 1  },        // 14
            };
            PAKs.SLG46534.cnt = new CNT[]
            {
                new CNT() { control = 0xA4, data = 0xC5, length = 16, selected = 1193 },    // 0
                new CNT() { control = 0xA5, data = 0xC7, length = 16, selected = 1192 },    // 1
                new CNT() { control = 0x9F, data = 0xC0, length = 8,  selected = 1198 },    // 2
                new CNT() { control = 0xA0, data = 0xC1, length = 8,  selected = 1197 },    // 3
                new CNT() { control = 0xA1, data = 0xC2, length = 8,  selected = 1196 },    // 4
                new CNT() { control = 0xA2, data = 0xC3, length = 8,  selected = 1195 },    // 5
                new CNT() { control = 0xA3, data = 0xC4, length = 8,  selected = 1194 },    // 6
            };
            PAKs.SLG46534.acmp = new ACMP[]
            {
                new ACMP() { control = 0xCB, hyst = 1174 },
                new ACMP() { control = 0xCC, hyst = 1170 },
                new ACMP() { control = 0xCD, hyst = 1181 },
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46535
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46535 = new PAK();
            PAKs.SLG46535.base_die = "SLG46535";
            PAKs.SLG46535.package = "STQFN-14";
            PAKs.SLG46535.package_size = "2mm x 2.2mm";
            PAKs.SLG46535.PAK_family = 5;
            PAKs.SLG46535.package_weight = "0.0068 g";
            PAKs.SLG46535.dual_supply = true;
            PAKs.SLG46535.vdd2_pin = 11;
            PAKs.SLG46535.pattern_id_address = 0xE6;
            PAKs.SLG46535.gnd_pin = 9;
            PAKs.SLG46535.pin = new PIN[] {
                new PIN() { address = 0x00, pin_type = "NA",      vdd_src = 0  },        // 0
                new PIN() { address = 0x00, pin_type = "VDD",     vdd_src = 0  },        // 1
                new PIN() { address = 0x80, pin_type = "GPI",     vdd_src = 1  },        // 2
                new PIN() { address = 0x82, pin_type = "GPIO",    vdd_src = 1  },        // 3
                new PIN() { address = 0x84, pin_type = "GPIO",    vdd_src = 1  },        // 4
                new PIN() { address = 0x85, pin_type = "GPIO_OE", vdd_src = 1  },        // 5
                new PIN() { address = 0x86, pin_type = "I2C",     vdd_src = 1  },        // 6
                new PIN() { address = 0x87, pin_type = "I2C",     vdd_src = 1  },        // 7
                new PIN() { address = 0x88, pin_type = "SD_OE",   vdd_src = 1  },        // 8
                new PIN() { address = 0x00, pin_type = "GND",     vdd_src = 0  },        // 9
                new PIN() { address = 0x89, pin_type = "SD",      vdd_src = 2  },        // 10
                new PIN() { address = 0x00, pin_type = "VDD2",    vdd_src = 0  },        // 11
                new PIN() { address = 0x8D, pin_type = "GPIO_OE", vdd_src = 2  },        // 12
                new PIN() { address = 0x8E, pin_type = "GPIO",    vdd_src = 2  },        // 13
                new PIN() { address = 0x91, pin_type = "GPIO",    vdd_src = 2  },        // 14
            };
            PAKs.SLG46535.cnt = new CNT[]
            {
                new CNT() { control = 0xA4, data = 0xC5, length = 16, selected = 1193 },    // 0
                new CNT() { control = 0xA5, data = 0xC7, length = 16, selected = 1192 },    // 1
                new CNT() { control = 0x9F, data = 0xC0, length = 8,  selected = 1198 },    // 2
                new CNT() { control = 0xA0, data = 0xC1, length = 8,  selected = 1197 },    // 3
                new CNT() { control = 0xA1, data = 0xC2, length = 8,  selected = 1196 },    // 4
                new CNT() { control = 0xA2, data = 0xC3, length = 8,  selected = 1195 },    // 5
                new CNT() { control = 0xA3, data = 0xC4, length = 8,  selected = 1194 },    // 6
            };
            PAKs.SLG46535.acmp = new ACMP[]
            {
                new ACMP() { control = 0xCB, hyst = 1174 },
                new ACMP() { control = 0xCC, hyst = 1170 },
                new ACMP() { control = 0xCD, hyst = 1181 },
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46536
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46536 = new PAK();
            PAKs.SLG46536.base_die = "SLG46536";
            PAKs.SLG46536.package = "STQFN-14";
            PAKs.SLG46536.package_size = "2mm x 2.2mm";
            PAKs.SLG46536.PAK_family = 5;
            PAKs.SLG46536.package_weight = "0.0066 g";
            PAKs.SLG46536.dual_supply = false;
            PAKs.SLG46536.pattern_id_address = 0xE6;
            PAKs.SLG46536.gnd_pin = 9;
            PAKs.SLG46536.pin = new PIN[] {
                new PIN() { address = 0x00, pin_type = "NA",      vdd_src = 0  },        // 0
                new PIN() { address = 0x00, pin_type = "VDD",     vdd_src = 0  },        // 1
                new PIN() { address = 0x80, pin_type = "GPI",     vdd_src = 1  },        // 2
                new PIN() { address = 0x82, pin_type = "GPIO",    vdd_src = 1  },        // 3
                new PIN() { address = 0x84, pin_type = "GPIO",    vdd_src = 1  },        // 4
                new PIN() { address = 0x85, pin_type = "GPIO_OE", vdd_src = 1  },        // 5
                new PIN() { address = 0x86, pin_type = "I2C",     vdd_src = 1  },        // 6
                new PIN() { address = 0x87, pin_type = "I2C",     vdd_src = 1  },        // 7
                new PIN() { address = 0x88, pin_type = "SD_OE",   vdd_src = 1  },        // 8
                new PIN() { address = 0x00, pin_type = "GND",     vdd_src = 0  },        // 9
                new PIN() { address = 0x89, pin_type = "SD",      vdd_src = 1  },        // 10
                new PIN() { address = 0x8B, pin_type = "GPIO_OE", vdd_src = 1  },        // 11
                new PIN() { address = 0x8D, pin_type = "GPIO_OE", vdd_src = 1  },        // 12
                new PIN() { address = 0x8E, pin_type = "GPIO",    vdd_src = 1  },        // 13
                new PIN() { address = 0x91, pin_type = "GPIO",    vdd_src = 1  },        // 14
            };
            PAKs.SLG46536.cnt = new CNT[]
            {
                new CNT() { control = 0xA4, data = 0xC5, length = 16, selected = 1193 },    // 0
                new CNT() { control = 0xA5, data = 0xC7, length = 16, selected = 1192 },    // 1
                new CNT() { control = 0x9F, data = 0xC0, length = 8,  selected = 1198 },    // 2
                new CNT() { control = 0xA0, data = 0xC1, length = 8,  selected = 1197 },    // 3
                new CNT() { control = 0xA1, data = 0xC2, length = 8,  selected = 1196 },    // 4
                new CNT() { control = 0xA2, data = 0xC3, length = 8,  selected = 1195 },    // 5
                new CNT() { control = 0xA3, data = 0xC4, length = 8,  selected = 1194 },    // 6
            };
            PAKs.SLG46536.acmp = new ACMP[]
            {
                new ACMP() { control = 0xCB, hyst = 1174 },
                new ACMP() { control = 0xCC, hyst = 1170 },
                new ACMP() { control = 0xCD, hyst = 1181 },
            };
        }
    }

    public static class PAKs
    {
        //GreenPAK5
        public static PAK SLG46531;
        public static PAK SLG46532;
        public static PAK SLG46533;
        public static PAK SLG46534;
        public static PAK SLG46535;
        public static PAK SLG46536;

        //GreenPAK4
        public static PAK SLG46140;
        public static PAK SLG46620;
        public static PAK SLG46621;

        //GreenPAK3
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