using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using UnityEngine;


public class CsvInfo
{
    [Name("name")]
    public string name { get; set; }
}


public class CsvParser
{
    const string DIR = "Definitions/";

    /// <summary>
    /// Read a csv file from "Resources/Definitions/<csv_name>.csv"
    /// </summary>
    public Dictionary<string, info_type> read<info_type>(string csv_name)
    {
        if (!typeof(CsvInfo).IsAssignableFrom(typeof(info_type)))
            throw new Exception();
        
        var file = new StringReader(Resources.Load<TextAsset>(DIR + csv_name).text);
        var data = new CsvReader(file, CultureInfo.InvariantCulture);

        data.Read();
        data.ReadHeader();
        Dictionary<string, info_type> infos = new();
        while (data.Read())
        {
            info_type info = data.GetRecord<info_type>();
            ItemInfo csv_info = (ItemInfo)Convert.ChangeType(info, typeof(ItemInfo));
            if (csv_info.name != "")
                infos[csv_info.name] = info;
        }

        return infos;
    }
}