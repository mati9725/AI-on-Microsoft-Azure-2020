using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHomeBot.Common.Enums
{
    public enum Intent
    {
        cancel = 1,
        set_power_state = 2,
        get_list = 3,
        None = 4,
        who_is_at_home = 5,
        get_weather = 6,
        check_internet_speed = 7,

    }
}
