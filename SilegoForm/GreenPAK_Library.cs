namespace GreenPAK_Library
{
    public class PAK
    {
        public string base_die;
        public string package;
        public string package_size;
        public byte PAK_family;
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
            public string pin_type;
            public int OE;
            public byte vdd_src;
            public string name;
            public string type;
            public string resistor;
            public string description;
        };

        public CNT[] cnt;

        public struct CNT
        {
            public int control;
            public int data;
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
            public int control;
            public int hyst;
            public int gain;
            public int low_bandwidth;
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
            PAKs.SLG46531.pattern_id_address = 1840;
            PAKs.SLG46531.dual_supply = false;
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
            PAKs.SLG46532.pattern_id_address = 1840;
            PAKs.SLG46532.dual_supply = true;
            PAKs.SLG46532.dual_supply_vdd_pins = "PIN2-PIN10";
            PAKs.SLG46532.dual_supply_vdd2_pins = "PIN12-PIN20";
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
            PAKs.SLG46533.pattern_id_address = 1840;
            PAKs.SLG46533.dual_supply = false;
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
            PAKs.SLG46534.pattern_id_address = 1840;
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
            PAKs.SLG46535.dual_supply_vdd_pins = "PIN2-PIN8";
            PAKs.SLG46535.dual_supply_vdd2_pins = "PIN10-PIN14";
            PAKs.SLG46535.pattern_id_address = 1840;
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
            PAKs.SLG46536.pattern_id_address = 1840;
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

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46140
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46140 = new PAK();
            PAKs.SLG46140.base_die = "SLG46140";
            PAKs.SLG46140.package = "STQFN-14";
            PAKs.SLG46140.package_size = "1.6mm x 2mm";
            PAKs.SLG46140.PAK_family = 4;
            PAKs.SLG46140.package_weight = "0.0090 g";  //###
            PAKs.SLG46140.pattern_id_address = 1007;
            PAKs.SLG46140.dual_supply = false;
            PAKs.SLG46140.pin = new PIN[] {
                new PIN() { address = 000, pin_type = "NA",                vdd_src = 0  },        // 0
                new PIN() { address = 000, pin_type = "VDD",               vdd_src = 0  },        // 1
                new PIN() { address = 761, pin_type = "GPI",               vdd_src = 1  },        // 2
                new PIN() { address = 766, pin_type = "GPIO_OE", OE = 270, vdd_src = 1  },        // 3
                new PIN() { address = 773, pin_type = "GPIO_OE", OE = 282, vdd_src = 1  },        // 4
                new PIN() { address = 780, pin_type = "GPIO_OE", OE = 294, vdd_src = 1  },        // 5
                new PIN() { address = 788, pin_type = "GPIO",              vdd_src = 1  },        // 6
                new PIN() { address = 795, pin_type = "GPIO_OE", OE = 312, vdd_src = 1  },        // 7
                new PIN() { address = 000, pin_type = "GND",               vdd_src = 0  },        // 8
                new PIN() { address = 802, pin_type = "SD_OE",   OE = 324, vdd_src = 1  },        // 9
                new PIN() { address = 811, pin_type = "SD",                vdd_src = 1  },        // 10
                new PIN() { address = 820, pin_type = "GPIO",              vdd_src = 1  },        // 11
                new PIN() { address = 827, pin_type = "GPIO_OE", OE = 348, vdd_src = 1  },        // 12
                new PIN() { address = 834, pin_type = "GPIO_OE", OE = 360, vdd_src = 1  },        // 13
                new PIN() { address = 841, pin_type = "GPIO_OE", OE = 372, vdd_src = 1  },        // 14
            };
            PAKs.SLG46140.cnt = new CNT[]
            {
                new CNT() { control = 737, data = 722, length = 14, selected = 743 },    // 0
                new CNT() { control = 714, data = 705, length = 8,  selected = 720 },    // 1
                new CNT() { control = 695, data = 680, length = 8,  selected = 701 },    // 2
                new CNT() { control = 670, data = 661, length = 8,  selected = 676 },    // 3
            };
            PAKs.SLG46140.acmp = new ACMP[]
            {
                new ACMP() { control = 496, hyst = 510, gain = 522, low_bandwidth = 524 },
                new ACMP() { control = 501, hyst = 508, gain = 519, low_bandwidth = 518 },
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46620
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46620 = new PAK();
            PAKs.SLG46620.base_die = "SLG46620";
            PAKs.SLG46620.package = "STQFN-20";
            PAKs.SLG46620.package_size = "2mm x 3mm";
            PAKs.SLG46620.PAK_family = 4;
            PAKs.SLG46620.package_weight = "0.015 g";
            PAKs.SLG46620.pattern_id_address = 2031;
            PAKs.SLG46620.dual_supply = false;
            PAKs.SLG46620.pin = new PIN[] {
                new PIN() { address = 0000, pin_type = "NA",                 vdd_src = 0  },        // 0
                new PIN() { address = 0000, pin_type = "VDD",                vdd_src = 0  },        // 1
                new PIN() { address = 0941, pin_type = "GPI",                vdd_src = 1  },        // 2
                new PIN() { address = 0946, pin_type = "GPIO_OE", OE = 0342, vdd_src = 1  },        // 3
                new PIN() { address = 0953, pin_type = "GPIO",               vdd_src = 1  },        // 4
                new PIN() { address = 0960, pin_type = "GPIO_OE", OE = 0360, vdd_src = 1  },        // 5
                new PIN() { address = 0967, pin_type = "GPIO",               vdd_src = 1  },        // 6
                new PIN() { address = 0974, pin_type = "GPIO_OE", OE = 0378, vdd_src = 1  },        // 7
                new PIN() { address = 0981, pin_type = "GPIO",               vdd_src = 1  },        // 8
                new PIN() { address = 0988, pin_type = "GPIO_OE", OE = 0396, vdd_src = 1  },        // 9
                new PIN() { address = 0995, pin_type = "SD_OE",   OE = 0408, vdd_src = 1  },        // 10
                new PIN() { address = 0000, pin_type = "GND",                vdd_src = 0  },        // 11
                new PIN() { address = 1911, pin_type = "SD",                 vdd_src = 1  },        // 12
                new PIN() { address = 1919, pin_type = "GPIO_OE", OE = 1372, vdd_src = 1  },        // 13
                new PIN() { address = 1926, pin_type = "GPIO_OE", OE = 1384, vdd_src = 1  },        // 14
                new PIN() { address = 1933, pin_type = "GPIO",               vdd_src = 1  },        // 15
                new PIN() { address = 1940, pin_type = "GPIO_OE", OE = 1402, vdd_src = 1  },        // 16
                new PIN() { address = 1947, pin_type = "GPIO",               vdd_src = 1  },        // 17
                new PIN() { address = 1954, pin_type = "GPIO_OE", OE = 1420, vdd_src = 1  },        // 18
                new PIN() { address = 1961, pin_type = "GPIO_OE", OE = 1432, vdd_src = 1  },        // 19
                new PIN() { address = 1968, pin_type = "GPIO",               vdd_src = 1  },        // 20
            };
            PAKs.SLG46620.cnt = new CNT[]
            {
                new CNT() { control = 1745, data = 1731, length = 14, selected = 1750 },    // 0
                new CNT() { control = 1767, data = 1753, length = 14, selected = 1772 },    // 1
                new CNT() { control = 1788, data = 1774, length = 14, selected = 1794 },    // 2
                new CNT() { control = 1813, data = 1799, length = 14, selected = 1818 },    // 3
                new CNT() { control = 1828, data = 1820, length = 08, selected = 1834 },    // 4
                new CNT() { control = 1846, data = 1838, length = 08, selected = 1851 },    // 5
                new CNT() { control = 1860, data = 1852, length = 08, selected = 1865 },    // 6
                new CNT() { control = 1874, data = 1866, length = 08, selected = 1879 },    // 7
                new CNT() { control = 1888, data = 1880, length = 08, selected = 1894 },    // 8
                new CNT() { control = 1903, data = 1895, length = 08, selected = 1909 },    // 9
            };
            PAKs.SLG46620.acmp = new ACMP[]
            {
                new ACMP() { control = 892, hyst = 934, gain = 853, low_bandwidth = 852 },  // 0
                new ACMP() { control = 897, hyst = 932, gain = 857, low_bandwidth = 861 },  // 1
                new ACMP() { control = 902, hyst = 930, gain = 864, low_bandwidth = 862 },  // 2
                new ACMP() { control = 907, hyst = 928, gain = 867, low_bandwidth = 866 },  // 3
                new ACMP() { control = 912, hyst = 926, gain = 871, low_bandwidth = 875 },  // 4
                new ACMP() { control = 917, hyst = 924, gain = 871, low_bandwidth = 880 },  // 5    //### ACMP5 has no gain option, fix this
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46621
            ////////////////////////////////////////////////////////////////////////////////////////////////////
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
            PAKs.SLG46621.pin = new PIN[] {
                new PIN() { address = 0000, pin_type = "NA",                 vdd_src = 0  },        // 0
                new PIN() { address = 0000, pin_type = "VDD",                vdd_src = 0  },        // 1
                new PIN() { address = 0941, pin_type = "GPI",                vdd_src = 1  },        // 2
                new PIN() { address = 0946, pin_type = "GPIO_OE", OE = 0342, vdd_src = 1  },        // 3
                new PIN() { address = 0953, pin_type = "GPIO",               vdd_src = 1  },        // 4
                new PIN() { address = 0960, pin_type = "GPIO_OE", OE = 0360, vdd_src = 1  },        // 5
                new PIN() { address = 0967, pin_type = "GPIO",               vdd_src = 1  },        // 6
                new PIN() { address = 0974, pin_type = "GPIO_OE", OE = 0378, vdd_src = 1  },        // 7
                new PIN() { address = 0981, pin_type = "GPIO",               vdd_src = 1  },        // 8
                new PIN() { address = 0988, pin_type = "GPIO_OE", OE = 0396, vdd_src = 1  },        // 9
                new PIN() { address = 0995, pin_type = "SD_OE",   OE = 0408, vdd_src = 1  },        // 10
                new PIN() { address = 0000, pin_type = "GND",                vdd_src = 0  },        // 11
                new PIN() { address = 1911, pin_type = "SD",                 vdd_src = 2  },        // 12
                new PIN() { address = 1919, pin_type = "GPIO_OE", OE = 1372, vdd_src = 2  },        // 13
                new PIN() { address = 0000, pin_type = "VDD2",               vdd_src = 0  },        // 14
                new PIN() { address = 1933, pin_type = "GPIO",               vdd_src = 2  },        // 15
                new PIN() { address = 1940, pin_type = "GPIO_OE", OE = 1402, vdd_src = 2  },        // 16
                new PIN() { address = 1947, pin_type = "GPIO",               vdd_src = 2  },        // 17
                new PIN() { address = 1954, pin_type = "GPIO_OE", OE = 1420, vdd_src = 2  },        // 18
                new PIN() { address = 1961, pin_type = "GPIO_OE", OE = 1432, vdd_src = 2  },        // 19
                new PIN() { address = 1968, pin_type = "GPIO",               vdd_src = 2  },        // 20
            };
            PAKs.SLG46621.cnt = new CNT[]
            {
                new CNT() { control = 1745, data = 1731, length = 14, selected = 1750 },    // 0
                new CNT() { control = 1767, data = 1753, length = 14, selected = 1772 },    // 1
                new CNT() { control = 1788, data = 1774, length = 14, selected = 1794 },    // 2
                new CNT() { control = 1813, data = 1799, length = 14, selected = 1818 },    // 3
                new CNT() { control = 1828, data = 1820, length = 08, selected = 1834 },    // 4
                new CNT() { control = 1846, data = 1838, length = 08, selected = 1851 },    // 5
                new CNT() { control = 1860, data = 1852, length = 08, selected = 1865 },    // 6
                new CNT() { control = 1874, data = 1866, length = 08, selected = 1879 },    // 7
                new CNT() { control = 1888, data = 1880, length = 08, selected = 1894 },    // 8
                new CNT() { control = 1903, data = 1895, length = 08, selected = 1909 },    // 9
            };
            PAKs.SLG46621.acmp = new ACMP[]
            {
                new ACMP() { control = 892, hyst = 934, gain = 853, low_bandwidth = 852 },  // 0
                new ACMP() { control = 897, hyst = 932, gain = 857, low_bandwidth = 861 },  // 1
                new ACMP() { control = 902, hyst = 930, gain = 864, low_bandwidth = 862 },  // 2
                new ACMP() { control = 907, hyst = 928, gain = 867, low_bandwidth = 866 },  // 3
                new ACMP() { control = 912, hyst = 926, gain = 871, low_bandwidth = 875 },  // 4
                new ACMP() { control = 917, hyst = 924, gain = 871, low_bandwidth = 880 },  // 5    //### ACMP5 has no gain option, fix this
            };

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //  SLG46721
            ////////////////////////////////////////////////////////////////////////////////////////////////////
            PAKs.SLG46721 = new PAK();
            PAKs.SLG46721.base_die = "SLG46721";
            PAKs.SLG46721.package = "STQFN-20";
            PAKs.SLG46721.package_size = "2mm x 3mm";
            PAKs.SLG46721.PAK_family = 3;
            PAKs.SLG46721.package_weight = "0.015 g";
            PAKs.SLG46721.pattern_id_address = 970; // ### check theProgram to make sure i updated this
            PAKs.SLG46721.dual_supply = false;
            PAKs.SLG46721.pin = new PIN[] {
                new PIN() { address = 000, pin_type = "NA",                vdd_src = 0  },        // 0
                new PIN() { address = 000, pin_type = "VDD",               vdd_src = 0  },        // 1
                new PIN() { address = 829, pin_type = "GPI",               vdd_src = 1  },        // 2
                new PIN() { address = 833, pin_type = "GPIO_OE", OE = 006, vdd_src = 1  },        // 3
                new PIN() { address = 840, pin_type = "GPIO",              vdd_src = 1  },        // 4
                new PIN() { address = 847, pin_type = "GPIO_OE", OE = 024, vdd_src = 1  },        // 5
                new PIN() { address = 854, pin_type = "GPIO",              vdd_src = 1  },        // 6
                new PIN() { address = 861, pin_type = "GPIO_OE", OE = 042, vdd_src = 1  },        // 7
                new PIN() { address = 868, pin_type = "GPIO",              vdd_src = 1  },        // 8
                new PIN() { address = 875, pin_type = "GPIO_OE", OE = 060, vdd_src = 1  },        // 9
                new PIN() { address = 882, pin_type = "SD",                vdd_src = 1  },        // 10
                new PIN() { address = 000, pin_type = "GND",               vdd_src = 0  },        // 11
                new PIN() { address = 890, pin_type = "SD",                vdd_src = 1  },        // 12
                new PIN() { address = 898, pin_type = "GPIO_OE", OE = 468, vdd_src = 1  },        // 13
                new PIN() { address = 905, pin_type = "GPIO_OE", OE = 480, vdd_src = 1  },        // 14
                new PIN() { address = 912, pin_type = "GPIO",              vdd_src = 1  },        // 15
                new PIN() { address = 919, pin_type = "GPIO_OE", OE = 498, vdd_src = 1  },        // 16
                new PIN() { address = 926, pin_type = "GPIO",              vdd_src = 1  },        // 17
                new PIN() { address = 933, pin_type = "GPIO_OE", OE = 516, vdd_src = 1  },        // 18
                new PIN() { address = 940, pin_type = "GPIO_OE", OE = 528, vdd_src = 1  },        // 19
                new PIN() { address = 947, pin_type = "GPIO",              vdd_src = 1  },        // 20
            };
            PAKs.SLG46721.cnt = new CNT[]
            {
                new CNT() { control = 0713, data = 0717, length = 14, selected = 0001 },    // 0
                new CNT() { control = 0733, data = 0737, length = 14, selected = 0001 },    // 1
                new CNT() { control = 0000, data = 0677, length = 08, selected = 0705 },    // 2
                new CNT() { control = 0000, data = 0693, length = 08, selected = 0706 },    // 3
                new CNT() { control = 0753, data = 0757, length = 08, selected = 0001 },    // 4
                new CNT() { control = 0767, data = 0771, length = 08, selected = 0001 },    // 5
                new CNT() { control = 0781, data = 0785, length = 08, selected = 0001 },    // 6
            };
            PAKs.SLG46721.acmp = new ACMP[]
            {
                new ACMP() { control = 0795, hyst = 0800, gain = 0802, low_bandwidth = 0804 },  // 0
                new ACMP() { control = 0806, hyst = 0811, gain = 0813, low_bandwidth = 0816 },  // 1
                new ACMP() { control = 0818, hyst = 0823, gain = 0825, low_bandwidth = 0827 },  // 2
                new ACMP() { control = 0552, hyst = 0557, gain = 0559, low_bandwidth = 0561 },  // 3
            };
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