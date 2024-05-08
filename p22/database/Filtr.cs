using System;
using System.Collections.Generic;

namespace p22.database;

public partial class Filtr
{
    public DateOnly ДатаОтъезда { get; set; }

    public decimal СтоимостьТура { get; set; }

    public string Страна { get; set; } = null!;

    public string Адрес { get; set; } = null!;

    public int Телефон { get; set; }
}
