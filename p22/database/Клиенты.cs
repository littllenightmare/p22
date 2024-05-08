using System;
using System.Collections.Generic;

namespace p22.database;

public partial class Клиенты
{
    public int ТабельныйНомер { get; set; }

    public string Фамилия { get; set; } = null!;

    public string Адрес { get; set; } = null!;

    public int Телефон { get; set; }

    public string? Фото { get; set; }

    public virtual ICollection<ТуристическиеТуры> ТуристическиеТурыs { get; set; } = new List<ТуристическиеТуры>();
}
