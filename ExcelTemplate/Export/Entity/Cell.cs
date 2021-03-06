﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ExportTemplate.Export.Entity
{
    public enum FillType
    {
        /// <summary>
        /// 原始大小
        /// </summary>
        Origin,
        /// <summary>
        /// 比例缩放
        /// </summary>
        Scale,
        /// <summary>
        /// 延伸填充
        /// </summary>
        Stretch
    }

    /// <summary>
    /// 填充单元格（孤立点）
    /// </summary>
    public class Cell : BaseEntity, IRuleEntity, ICloneable<Cell>
    {
        private string _sourceName;
        private string _field;
        private int _dataIndex = 0;
        private bool _valueAppend = false;
        private string _value;
        private FillType _fillType = FillType.Origin;

        public FillType FillType
        {
            get { return _fillType; }
            set { _fillType = value; }
        }

        public string SourceName
        {
            get { return _sourceName; }
            set { _sourceName = value; }
        }
        //数据源字段
        public string Field
        {
            get { return _field; }
            set { _field = value; }
        }//数据行索引

        public int DataIndex
        {
            get { return _dataIndex; }
            set { _dataIndex = value; }
        }
        public bool ValueAppend
        {
            get { return _valueAppend; }
            set { _valueAppend = value; }
        }
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Cell(ProductRule productRule, BaseEntity container, Location location) : base(productRule, container, location) { }

        public override string ToString()
        {
            return string.Format("<Cell location=\"{0}\"{1} />", _location,
                (!string.IsNullOrEmpty(SourceName) ? string.Format(" source=\"{0}\"", SourceName) : string.Empty) +
                (DataIndex > 0 ? string.Format(" index=\"{0}\"", DataIndex) : string.Empty) +
                (ValueAppend ? string.Format(" valueAppend=\"true\"", ValueAppend.ToString().ToLower()) : string.Empty) +
                (!string.IsNullOrEmpty(Value) ? string.Format(" value=\"{0}\"", Value) : string.Empty));
        }

        public override BaseEntity Clone(ProductRule productRule, BaseEntity container)
        {
            Cell tmpCell = new Cell(productRule, container, _location.Clone())
            {
                SourceName = SourceName,
                Field = Field,
                Value = Value,
                DataIndex = DataIndex,
                ValueAppend = ValueAppend,
                FillType = FillType
            };
            //if (Source != null)
            //{
            //    tmpCell.Source = productRule.GetSource(Source.Name);
            //}
            return tmpCell;
        }

        public Cell CloneEntity(ProductRule productRule, BaseEntity container)
        {
            return this.Clone(productRule, container) as Cell;
        }
    }
}
