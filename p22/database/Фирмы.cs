using System;
using System.Collections.Generic;

namespace p22.database;

public partial class Фирмы
{
    public int Код { get; set; }

    public string Название { get; set; } = null!;

    public string? Адрес { get; set; }

    public string? ФамилияДиректора { get; set; }

    public virtual ICollection<ТуристическиеТуры> ТуристическиеТурыs { get; set; } = new List<ТуристическиеТуры>();
}
