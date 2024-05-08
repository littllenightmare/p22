using System;
using System.Collections.Generic;

namespace p22.database;

public partial class ТуристическиеТуры
{
    public string Страна { get; set; } = null!;

    public DateOnly ДатаОтъезда { get; set; }

    public decimal СтоимостьТура { get; set; }

    public int Клиент { get; set; }

    public int Фирма { get; set; }

    public virtual Клиенты КлиентNavigation { get; set; } = null!;

    public virtual Фирмы ФирмаNavigation { get; set; } = null!;
}
