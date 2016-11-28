using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Catalogos.Models
{
    public class Column
    {
        public string name { get; set; }
        public string index { get; set; }
        public int width { get; set; }
        public string align { get; set; }

        public Column(string _name, string _index)
        {
            name = _name;
            index = _index;
        }

        public Column(string _name, string _index, int _width, string _align)
        {
            name = _name;
            index = _index;
            width = _width;
            align = _align;
        }
    }

    public class Row
    {
        public int id { get; set; }
        public string[] cell { get; set; }
    }

    public class GridData
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public Row[] rows { get; set; }

        public GridData(int _total, int _page, int _records, Row[] _rows)
        {
            total = _total;
            page = _page;
            records = _records;
            rows = _rows;
        }
    }

    public class Grid
    {
        public string url { get; set; }
        public string datatype { get; set; }
        public string mtype { get; set; }
        public string[] colNames { get; set; }
        public Column[] colModel { get; set; }
        public string pager { get; set; }
        public int rowNum { get; set; }
        public int[] rowList { get; set; }
        public string sortname { get; set; }
        public string sortorder { get; set; }
        public bool viewrecords { get; set; }
        public string caption { get; set; }

        public Grid(string _url, string[] columnNames, Column[] columnModel, string sortName, string _caption)
        {
            url = _url;
            datatype = "json";
            mtype = "GET";
            colNames = columnNames;
            colModel = columnModel;
            pager = null;
            rowNum = 10;
            rowList = new int[4] { 5, 10, 20, 50 };
            sortname = sortName;
            sortorder = "desc";
            viewrecords = true;
            caption = _caption;
        }
    }
    
}