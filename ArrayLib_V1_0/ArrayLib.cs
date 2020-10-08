using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace ArrayLib_V1_0
{
    /// <summary>
    /// This class contains some tools to build and manage a array or jagged array
    /// </summary>
    public static class ArrayLib
    {
        /// <summary>
        /// Creat a jagged array from a csv file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="splitTerm"></param>
        /// <param name="encoding"></param>
        /// <param name="type"></param>
        /// <returns> Return a <see cref="object[][]"/> with informed type </returns>
        public static object GetJaggedArrayFromFile(ValueTypes convertType, string path, char splitTerm, Encoding encoding)
        {

            if (Path.GetExtension(path).ToLower().Contains("csv"))
            {
                try
                {
                    object jaggedArray = NewJaggedArray(convertType);
                    using (StreamReader sR = new StreamReader(path, true))
                    {
                        while (!sR.EndOfStream)
                        {
                            object array = NewArray(convertType);
                            foreach (string cellValue in sR.ReadLine().Split(splitTerm))
                            {
                                AddValueToArray(convertType, ConvertValue(convertType, cellValue), ref array);
                            }
                            AddArrayToJaggedArray(convertType, array, ref jaggedArray);
                        }
                    }
                    return jaggedArray;
                }
                catch
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Convert a jagged array to a unidimensional array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object ConvertJaggedArrayToArray(ValueTypes type, object jaggedArray)
        {
            int rowSize = GetJaggedArrayRowSize(type, jaggedArray);
            int columnSize = GetJaggedArrayColumnSize(type, jaggedArray);
            object array = NewArray(type, rowSize * columnSize);

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    AddValueToArray(type, GetJaggedArrayValue(type, i, j, jaggedArray), ref array);
                }
            }

            return array;
        }

        /// <summary>
        /// Convert a array to a jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static object ConvertArrayToJaggedArray(ValueTypes type, object array)
        {
            int rowSize = GetArrayLenght(type, array);
            int columnSize = 1;
            object jaggedArray = NewJaggedArray(type, rowSize, columnSize);

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    InsertValueToJaggedArray(type, GetArrayValue(type, i, array),i,j, ref jaggedArray);
                }
            }

            return jaggedArray;
        }


        /// <summary>
        /// Convert um jagged array to other jagged Array, with different type
        /// </summary>
        /// <param name="currentType"></param>
        /// <param name="newType"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object ConvertJaggedArrayToNewJaggedArrayType(ValueTypes currentType, ValueTypes newType, object jaggedArray)
        {
            try
            {
                int rowSize = GetJaggedArrayRowSize(currentType, jaggedArray);
                int columnSize = GetJaggedArrayColumnSize(currentType, jaggedArray);
                object newJaggedArray = NewJaggedArray(newType, rowSize, columnSize);
                for (int i = 0; i < rowSize; i++)
                {
                    for (int j = 0; j < columnSize; j++)
                    {
                        InsertValueToJaggedArray(newType, ConvertValue(newType, GetJaggedArrayValue(currentType, i, j, jaggedArray)), i, j, ref newJaggedArray);
                    }
                }
                return newJaggedArray;
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Get the value from a array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>        
        /// <param name="array"></param>
        /// <returns></returns>
        public static object GetArrayValue(ValueTypes type, int index, object array)
        {
            switch (type)
            {
                case ValueTypes.SByte: return ((sbyte[])array)[index];
                case ValueTypes.Byte: return ((byte[])array)[index];
                case ValueTypes.UShort: return ((ushort[])array)[index];
                case ValueTypes.Short: return ((short[])array)[index];
                case ValueTypes.UInt: return ((uint[])array)[index];
                case ValueTypes.Int: return ((int[])array)[index];
                case ValueTypes.ULong: return ((ulong[])array)[index];
                case ValueTypes.Long: return ((long[])array)[index];
                case ValueTypes.Float: return ((float[])array)[index];
                case ValueTypes.Double: return ((double[])array)[index];
                case ValueTypes.Char: return ((char[])array)[index];
                case ValueTypes.String: return ((string[])array)[index];
                case ValueTypes.Bool: return ((bool[])array)[index];
                case ValueTypes.Object: return ((object[])array)[index];
                default: return null;
            }
        }

        /// <summary>
        /// Get the lenght from a array
        /// </summary>
        /// <param name="type"></param>      
        /// <param name="array"></param>
        /// <returns></returns>
        public static int GetArrayLenght(ValueTypes type, object array)
        {
            switch (type)
            {
                case ValueTypes.SByte: return ((sbyte[])array).Length;
                case ValueTypes.Byte: return ((byte[])array).Length;
                case ValueTypes.UShort: return ((ushort[])array).Length;
                case ValueTypes.Short: return ((short[])array).Length;
                case ValueTypes.UInt: return ((uint[])array).Length;
                case ValueTypes.Int: return ((int[])array).Length;
                case ValueTypes.ULong: return ((ulong[])array).Length;
                case ValueTypes.Long: return ((long[])array).Length;
                case ValueTypes.Float: return ((float[])array).Length;
                case ValueTypes.Double: return ((double[])array).Length;
                case ValueTypes.Char: return ((char[])array).Length;
                case ValueTypes.String: return ((string[])array).Length;
                case ValueTypes.Bool: return ((bool[])array).Length;
                case ValueTypes.Object: return ((object[])array).Length;
                default: return -1;
            }
        }

        /// <summary>
        /// Get the cell value from a jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object GetJaggedArrayValue(ValueTypes type, int rowIndex, int columnIndex, object jaggedArray)
        {
            switch (type)
            {
                case ValueTypes.SByte: return ((sbyte[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Byte: return ((byte[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.UShort: return ((ushort[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Short: return ((short[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.UInt: return ((uint[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Int: return ((int[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.ULong: return ((ulong[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Long: return ((long[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Float: return ((float[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Double: return ((double[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Char: return ((char[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.String: return ((string[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Bool: return ((bool[][])jaggedArray)[rowIndex][columnIndex];
                case ValueTypes.Object: return ((object[][])jaggedArray)[rowIndex][columnIndex];
                default: return null;
            }
        }

        /// <summary>
        /// Get a specific row value of a jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rowIndex"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object GetJaggedArrayRow(ValueTypes type, int rowIndex, object jaggedArray)
        {
            switch (type)
            {
                case ValueTypes.SByte: return ((sbyte[][])jaggedArray)[rowIndex];
                case ValueTypes.Byte: return ((byte[][])jaggedArray)[rowIndex];
                case ValueTypes.UShort: return ((ushort[][])jaggedArray)[rowIndex];
                case ValueTypes.Short: return ((short[][])jaggedArray)[rowIndex];
                case ValueTypes.UInt: return ((uint[][])jaggedArray)[rowIndex];
                case ValueTypes.Int: return ((int[][])jaggedArray)[rowIndex];
                case ValueTypes.ULong: return ((ulong[][])jaggedArray)[rowIndex];
                case ValueTypes.Long: return ((long[][])jaggedArray)[rowIndex];
                case ValueTypes.Float: return ((float[][])jaggedArray)[rowIndex];
                case ValueTypes.Double: return ((double[][])jaggedArray)[rowIndex];
                case ValueTypes.Char: return ((char[][])jaggedArray)[rowIndex];
                case ValueTypes.String: return ((string[][])jaggedArray)[rowIndex];
                case ValueTypes.Bool: return ((bool[][])jaggedArray)[rowIndex];
                case ValueTypes.Object: return ((object[][])jaggedArray)[rowIndex];
                default: return null;
            }
        }

        /// <summary>
        /// Get a specific column value of a jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnIndex"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object GetJaggedArrayColumn(ValueTypes type, int columnIndex, object jaggedArray)
        {
            object columnArray = NewArray(type);
            switch (type)
            {
                case ValueTypes.SByte: foreach (sbyte[] array in ((sbyte[][])jaggedArray)) { columnArray = ((sbyte[])columnArray).Append(((sbyte[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Byte: foreach (byte[] array in ((byte[][])jaggedArray)) { columnArray = ((byte[])columnArray).Append(((byte[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.UShort: foreach (ushort[] array in ((ushort[][])jaggedArray)) { columnArray = ((ushort[])columnArray).Append(((ushort[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Short: foreach (short[] array in ((short[][])jaggedArray)) { columnArray = ((short[])columnArray).Append(((short[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.UInt: foreach (uint[] array in ((uint[][])jaggedArray)) { columnArray = ((uint[])columnArray).Append(((uint[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Int: foreach (int[] array in ((int[][])jaggedArray)) { columnArray = ((int[])columnArray).Append(((int[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.ULong: foreach (ulong[] array in ((ulong[][])jaggedArray)) { columnArray = ((ulong[])columnArray).Append(((ulong[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Long: foreach (long[] array in ((long[][])jaggedArray)) { columnArray = ((long[])columnArray).Append(((long[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Float: foreach (float[] array in ((float[][])jaggedArray)) { columnArray = ((float[])columnArray).Append(((float[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Double: foreach (double[] array in ((double[][])jaggedArray)) { columnArray = ((double[])columnArray).Append(((double[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Char: foreach (char[] array in ((char[][])jaggedArray)) { columnArray = ((char[])columnArray).Append(((char[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.String: foreach (string[] array in ((string[][])jaggedArray)) { columnArray = ((string[])columnArray).Append(((string[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Bool: foreach (bool[] array in ((bool[][])jaggedArray)) { columnArray = ((bool[])columnArray).Append(((bool[])array)[columnIndex]).ToArray(); } return columnArray;
                case ValueTypes.Object: foreach (object[] array in ((object[][])jaggedArray)) { columnArray = ((object[])columnArray).Append(((object[])array)[columnIndex]).ToArray(); } return columnArray;
                default: return null;
            }
        }

        /// <summary>
        /// Get the a new jagged array, with a new row and column size, from a already existing jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="initialRowIndex"></param>
        /// <param name="rowLenght"></param>
        /// <param name="initialColumnIndex"></param>
        /// <param name="columnLenght"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object GetSubJaggedArray(ValueTypes type, int initialRowIndex, int rowLenght, int initialColumnIndex, int columnLenght, object jaggedArray)
        {
            object subJaggedArray = NewJaggedArray(type);
            for (int i = initialRowIndex; i < rowLenght; i++)
            {
                object subArray = NewArray(type);
                for (int j = initialColumnIndex; j < columnLenght; j++)
                {
                    AddValueToArray(type, GetJaggedArrayValue(type, i, j, jaggedArray), ref subArray);
                }
                AddArrayToJaggedArray(type, subArray, ref subJaggedArray);
            }
            return subJaggedArray;
        }
        /// <summary>
        /// Get the a new jagged array, with a new row and column size, from a already existing jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="initialRowIndex"></param>
        /// <param name="initialColumnIndex"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object GetSubJaggedArray(ValueTypes type, int initialRowIndex, int initialColumnIndex, object jaggedArray)
        {
            object subJaggedArray = NewJaggedArray(type);
            for (int i = initialRowIndex; i < GetJaggedArrayRowSize(type, jaggedArray); i++)
            {
                object subArray = NewArray(type);
                for (int j = initialColumnIndex; j < GetJaggedArrayColumnSize(type, jaggedArray); j++)
                {
                    AddValueToArray(type, GetJaggedArrayValue(type, i, j, jaggedArray), ref subArray);
                }
                AddArrayToJaggedArray(type, subArray, ref subJaggedArray);
            }
            return subJaggedArray;
        }

        /// <summary>
        /// Get the jagged array column size
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static int GetJaggedArrayColumnSize(ValueTypes type, object jaggedArray)
        {
            switch (type)
            {
                case ValueTypes.SByte: return ((sbyte[][])jaggedArray).First().Length;
                case ValueTypes.Byte: return ((byte[][])jaggedArray).First().Length;
                case ValueTypes.UShort: return ((ushort[][])jaggedArray).First().Length;
                case ValueTypes.Short: return ((short[][])jaggedArray).First().Length;
                case ValueTypes.UInt: return ((uint[][])jaggedArray).First().Length;
                case ValueTypes.Int: return ((int[][])jaggedArray).First().Length;
                case ValueTypes.ULong: return ((ulong[][])jaggedArray).First().Length;
                case ValueTypes.Long: return ((long[][])jaggedArray).First().Length;
                case ValueTypes.Float: return ((float[][])jaggedArray).First().Length;
                case ValueTypes.Double: return ((double[][])jaggedArray).First().Length;
                case ValueTypes.Char: return ((char[][])jaggedArray).First().Length;
                case ValueTypes.String: return ((string[][])jaggedArray).First().Length;
                case ValueTypes.Bool: return ((bool[][])jaggedArray).First().Length;
                case ValueTypes.Object: return ((object[][])jaggedArray).First().Length;
                default: return -1;
            }
        }
        /// <summary>
        /// Get the jagged array row size
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static int GetJaggedArrayRowSize(ValueTypes type, object jaggedArray)
        {
            switch (type)
            {
                case ValueTypes.SByte: return ((sbyte[][])jaggedArray).Length;
                case ValueTypes.Byte: return ((byte[][])jaggedArray).Length;
                case ValueTypes.UShort: return ((ushort[][])jaggedArray).Length;
                case ValueTypes.Short: return ((short[][])jaggedArray).Length;
                case ValueTypes.UInt: return ((uint[][])jaggedArray).Length;
                case ValueTypes.Int: return ((int[][])jaggedArray).Length;
                case ValueTypes.ULong: return ((ulong[][])jaggedArray).Length;
                case ValueTypes.Long: return ((long[][])jaggedArray).Length;
                case ValueTypes.Float: return ((float[][])jaggedArray).Length;
                case ValueTypes.Double: return ((double[][])jaggedArray).Length;
                case ValueTypes.Char: return ((char[][])jaggedArray).Length;
                case ValueTypes.String: return ((string[][])jaggedArray).Length;
                case ValueTypes.Bool: return ((bool[][])jaggedArray).Length;
                case ValueTypes.Object: return ((object[][])jaggedArray).Length;
                default: return -1;
            }
        }

        /// <summary>
        /// Add a new array to a alredy existing jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="valueArray"></param>
        /// <param name="jaggedArray"></param>
        public static void AddArrayToJaggedArray(ValueTypes type, object valueArray, ref object jaggedArray)
        {
            switch (type)
            {
                case ValueTypes.SByte: jaggedArray = ((sbyte[][])jaggedArray).Append((sbyte[])valueArray).ToArray(); return;
                case ValueTypes.Byte: jaggedArray = ((byte[][])jaggedArray).Append((byte[])valueArray).ToArray(); return;
                case ValueTypes.UShort: jaggedArray = ((ushort[][])jaggedArray).Append((ushort[])valueArray).ToArray(); return;
                case ValueTypes.Short: jaggedArray = ((short[][])jaggedArray).Append((short[])valueArray).ToArray(); return;
                case ValueTypes.UInt: jaggedArray = ((uint[][])jaggedArray).Append((uint[])valueArray).ToArray(); return;
                case ValueTypes.Int: jaggedArray = ((int[][])jaggedArray).Append((int[])valueArray).ToArray(); return;
                case ValueTypes.ULong: jaggedArray = ((ulong[][])jaggedArray).Append((ulong[])valueArray).ToArray(); return;
                case ValueTypes.Long: jaggedArray = ((long[][])jaggedArray).Append((long[])valueArray).ToArray(); return;
                case ValueTypes.Float: jaggedArray = ((float[][])jaggedArray).Append((float[])valueArray).ToArray(); return;
                case ValueTypes.Double: jaggedArray = ((double[][])jaggedArray).Append((double[])valueArray).ToArray(); return;
                case ValueTypes.Char: jaggedArray = ((char[][])jaggedArray).Append((char[])valueArray).ToArray(); return;
                case ValueTypes.String: jaggedArray = ((string[][])jaggedArray).Append((string[])valueArray).ToArray(); return;
                case ValueTypes.Bool: jaggedArray = ((bool[][])jaggedArray).Append((bool[])valueArray).ToArray(); return;
                case ValueTypes.Object: jaggedArray = ((object[][])jaggedArray).Append((object[])valueArray).ToArray(); return;
            }
        }
        /// <summary>
        /// Add a new value to a alredy existing array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="array"></param>
        public static void AddValueToArray(ValueTypes type, object value, ref object array)
        {
            switch (type)
            {
                case ValueTypes.SByte: array = ((sbyte[])array).Append((sbyte)value).ToArray(); return;
                case ValueTypes.Byte: array = ((byte[])array).Append((byte)value).ToArray(); return;
                case ValueTypes.UShort: array = ((ushort[])array).Append((ushort)value).ToArray(); return;
                case ValueTypes.Short: array = ((short[])array).Append((short)value).ToArray(); return;
                case ValueTypes.UInt: array = ((uint[])array).Append((uint)value).ToArray(); return;
                case ValueTypes.Int: array = ((int[])array).Append((int)value).ToArray(); return;
                case ValueTypes.ULong: array = ((ulong[])array).Append((ulong)value).ToArray(); return;
                case ValueTypes.Long: array = ((long[])array).Append((long)value).ToArray(); return;
                case ValueTypes.Float: array = ((float[])array).Append((float)value).ToArray(); return;
                case ValueTypes.Double: array = ((double[])array).Append((double)value).ToArray(); return;
                case ValueTypes.Char: array = ((char[])array).Append((char)value).ToArray(); return;
                case ValueTypes.String: array = ((string[])array).Append((string)value).ToArray(); return;
                case ValueTypes.Bool: array = ((bool[])array).Append((bool)value).ToArray(); return;
                case ValueTypes.Object: array = ((object[])array).Append(value).ToArray(); return;
            }
        }



        /// <summary>
        /// Insert a value into a specified index of a jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <param name="array"></param>
        public static void InsertValueToJaggedArray(ValueTypes type, object value, int rowIndex, int columnIndex, ref object array)
        {
            switch (type)
            {
                case ValueTypes.SByte: ((sbyte[][])array)[rowIndex][columnIndex] = (sbyte)value; return;
                case ValueTypes.Byte: ((byte[][])array)[rowIndex][columnIndex] = (byte)value; return;
                case ValueTypes.UShort: ((ushort[][])array)[rowIndex][columnIndex] = (ushort)value; return;
                case ValueTypes.Short: ((short[][])array)[rowIndex][columnIndex] = (short)value; return;
                case ValueTypes.UInt: ((uint[][])array)[rowIndex][columnIndex] = (uint)value; return;
                case ValueTypes.Int: ((int[][])array)[rowIndex][columnIndex] = (int)value; return;
                case ValueTypes.ULong: ((ulong[][])array)[rowIndex][columnIndex] = (ulong)value; return;
                case ValueTypes.Long: ((long[][])array)[rowIndex][columnIndex] = (long)value; return;
                case ValueTypes.Float: ((float[][])array)[rowIndex][columnIndex] = (float)value; return;
                case ValueTypes.Double: ((double[][])array)[rowIndex][columnIndex] = (double)value; return;
                case ValueTypes.Char: ((char[][])array)[rowIndex][columnIndex] = (char)value; return;
                case ValueTypes.String: ((string[][])array)[rowIndex][columnIndex] = (string)value; return;
                case ValueTypes.Bool: ((bool[][])array)[rowIndex][columnIndex] = (bool)value; return;
                case ValueTypes.Object: ((object[][])array)[rowIndex][columnIndex] = (object)value; return;
                default: return;
            }
        }

        /// <summary>
        /// Insert a value into a specified index of a array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <param name="array"></param>
        public static void InsertValueToArray(ValueTypes type, object value, long index, ref object array)
        {
            switch (type)
            {
                case ValueTypes.SByte: ((sbyte[])array)[index] = (sbyte)value; return;
                case ValueTypes.Byte: ((byte[])array)[index] = (byte)value; return;
                case ValueTypes.UShort: ((ushort[])array)[index] = (ushort)value; return;
                case ValueTypes.Short: ((short[])array)[index] = (short)value; return;
                case ValueTypes.UInt: ((uint[])array)[index] = (uint)value; return;
                case ValueTypes.Int: ((int[])array)[index] = (int)value; return;
                case ValueTypes.ULong: ((ulong[])array)[index] = (ulong)value; return;
                case ValueTypes.Long: ((long[])array)[index] = (long)value; return;
                case ValueTypes.Float: ((float[])array)[index] = (float)value; return;
                case ValueTypes.Double: ((double[])array)[index] = (double)value; return;
                case ValueTypes.Char: ((char[])array)[index] = (char)value; return;
                case ValueTypes.String: ((string[])array)[index] = (string)value; return;
                case ValueTypes.Bool: ((bool[])array)[index] = (bool)value; return;
                case ValueTypes.Object: ((object[])array)[index] = (object)value; return;
                default: return;
            }
        }

        /// <summary>
        /// Create a new agged array
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object NewJaggedArray(ValueTypes type)
        {
            switch (type)
            {
                case ValueTypes.SByte: return new sbyte[][] { };
                case ValueTypes.Byte: return new byte[][] { };
                case ValueTypes.UShort: return new ushort[][] { };
                case ValueTypes.Short: return new short[][] { };
                case ValueTypes.UInt: return new uint[][] { };
                case ValueTypes.Int: return new int[][] { };
                case ValueTypes.ULong: return new ulong[][] { };
                case ValueTypes.Long: return new long[][] { };
                case ValueTypes.Float: return new float[][] { };
                case ValueTypes.Double: return new double[][] { };
                case ValueTypes.Char: return new char[][] { };
                case ValueTypes.String: return new string[][] { };
                case ValueTypes.Bool: return new bool[][] { };
                case ValueTypes.Object: return new object[][] { };
                default: return null;
            }
        }
        /// <summary>
        /// Create a new agged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rowSize"></param>
        /// <returns></returns>
        public static object NewJaggedArray(ValueTypes type, int rowSize)
        {
            switch (type)
            {
                case ValueTypes.SByte: return new sbyte[rowSize][];
                case ValueTypes.Byte: return new byte[rowSize][];
                case ValueTypes.UShort: return new ushort[rowSize][];
                case ValueTypes.Short: return new short[rowSize][];
                case ValueTypes.UInt: return new uint[rowSize][];
                case ValueTypes.Int: return new int[rowSize][];
                case ValueTypes.ULong: return new ulong[rowSize][];
                case ValueTypes.Long: return new long[rowSize][];
                case ValueTypes.Float: return new float[rowSize][];
                case ValueTypes.Double: return new double[rowSize][];
                case ValueTypes.Char: return new char[rowSize][];
                case ValueTypes.String: return new string[rowSize][];
                case ValueTypes.Bool: return new bool[rowSize][];
                case ValueTypes.Object: return new object[rowSize][];
                default: return null;
            }
        }
        /// <summary>
        /// Create a new agged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rowSize"></param>
        /// <param name="columnSize"></param>
        /// <returns></returns>
        public static object NewJaggedArray(ValueTypes type, int rowSize, int columnSize)
        {
            object jaggedArray = null;
            switch (type)
            {
                case ValueTypes.SByte: jaggedArray = new sbyte[rowSize][]; for (int i = 0; i < ((sbyte[][])jaggedArray).Length; i++) { ((sbyte[][])jaggedArray)[i] = (sbyte[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Byte: jaggedArray = new byte[rowSize][]; for (int i = 0; i < ((byte[][])jaggedArray).Length; i++) { ((byte[][])jaggedArray)[i] = (byte[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.UShort: jaggedArray = new ushort[rowSize][]; for (int i = 0; i < ((ushort[][])jaggedArray).Length; i++) { ((ushort[][])jaggedArray)[i] = (ushort[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Short: jaggedArray = new short[rowSize][]; for (int i = 0; i < ((short[][])jaggedArray).Length; i++) { ((short[][])jaggedArray)[i] = (short[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.UInt: jaggedArray = new uint[rowSize][]; for (int i = 0; i < ((uint[][])jaggedArray).Length; i++) { ((uint[][])jaggedArray)[i] = (uint[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Int: jaggedArray = new int[rowSize][]; for (int i = 0; i < ((int[][])jaggedArray).Length; i++) { ((int[][])jaggedArray)[i] = (int[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.ULong: jaggedArray = new ulong[rowSize][]; for (int i = 0; i < ((ulong[][])jaggedArray).Length; i++) { ((ulong[][])jaggedArray)[i] = (ulong[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Long: jaggedArray = new long[rowSize][]; for (int i = 0; i < ((long[][])jaggedArray).Length; i++) { ((long[][])jaggedArray)[i] = (long[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Float: jaggedArray = new float[rowSize][]; for (int i = 0; i < ((float[][])jaggedArray).Length; i++) { ((float[][])jaggedArray)[i] = (float[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Double: jaggedArray = new double[rowSize][]; for (int i = 0; i < ((double[][])jaggedArray).Length; i++) { ((double[][])jaggedArray)[i] = (double[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Char: jaggedArray = new char[rowSize][]; for (int i = 0; i < ((char[][])jaggedArray).Length; i++) { ((char[][])jaggedArray)[i] = (char[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.String: jaggedArray = new string[rowSize][]; for (int i = 0; i < ((string[][])jaggedArray).Length; i++) { ((string[][])jaggedArray)[i] = (string[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Bool: jaggedArray = new bool[rowSize][]; for (int i = 0; i < ((bool[][])jaggedArray).Length; i++) { ((bool[][])jaggedArray)[i] = (bool[])NewArray(type, columnSize); } return jaggedArray;
                case ValueTypes.Object: jaggedArray = new object[rowSize][]; for (int i = 0; i < ((object[][])jaggedArray).Length; i++) { ((object[][])jaggedArray)[i] = (object[])NewArray(type, columnSize); } return jaggedArray;
                default: return null;
            }
        }
        /// <summary>
        /// Create a new agged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rowSize"></param>
        /// <param name="columnSize"></param>
        /// <param name="presetValue"></param>
        /// <returns></returns>
        public static object NewJaggedArray(ValueTypes type, int rowSize, int columnSize, object presetValue)
        {
            object jaggedArray = null;
            switch (type)
            {
                case ValueTypes.SByte: jaggedArray = new sbyte[rowSize][]; for (int i = 0; i < ((sbyte[][])jaggedArray).Length; i++) { ((sbyte[][])jaggedArray)[i] = (sbyte[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Byte: jaggedArray = new byte[rowSize][]; for (int i = 0; i < ((byte[][])jaggedArray).Length; i++) { ((byte[][])jaggedArray)[i] = (byte[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.UShort: jaggedArray = new ushort[rowSize][]; for (int i = 0; i < ((ushort[][])jaggedArray).Length; i++) { ((ushort[][])jaggedArray)[i] = (ushort[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Short: jaggedArray = new short[rowSize][]; for (int i = 0; i < ((short[][])jaggedArray).Length; i++) { ((short[][])jaggedArray)[i] = (short[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.UInt: jaggedArray = new uint[rowSize][]; for (int i = 0; i < ((uint[][])jaggedArray).Length; i++) { ((uint[][])jaggedArray)[i] = (uint[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Int: jaggedArray = new int[rowSize][]; for (int i = 0; i < ((int[][])jaggedArray).Length; i++) { ((int[][])jaggedArray)[i] = (int[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.ULong: jaggedArray = new ulong[rowSize][]; for (int i = 0; i < ((ulong[][])jaggedArray).Length; i++) { ((ulong[][])jaggedArray)[i] = (ulong[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Long: jaggedArray = new long[rowSize][]; for (int i = 0; i < ((long[][])jaggedArray).Length; i++) { ((long[][])jaggedArray)[i] = (long[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Float: jaggedArray = new float[rowSize][]; for (int i = 0; i < ((float[][])jaggedArray).Length; i++) { ((float[][])jaggedArray)[i] = (float[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Double: jaggedArray = new double[rowSize][]; for (int i = 0; i < ((double[][])jaggedArray).Length; i++) { ((double[][])jaggedArray)[i] = (double[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Char: jaggedArray = new char[rowSize][]; for (int i = 0; i < ((char[][])jaggedArray).Length; i++) { ((char[][])jaggedArray)[i] = (char[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.String: jaggedArray = new string[rowSize][]; for (int i = 0; i < ((string[][])jaggedArray).Length; i++) { ((string[][])jaggedArray)[i] = (string[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Bool: jaggedArray = new bool[rowSize][]; for (int i = 0; i < ((bool[][])jaggedArray).Length; i++) { ((bool[][])jaggedArray)[i] = (bool[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                case ValueTypes.Object: jaggedArray = new object[rowSize][]; for (int i = 0; i < ((object[][])jaggedArray).Length; i++) { ((object[][])jaggedArray)[i] = (object[])NewArray(type, columnSize, presetValue); } return jaggedArray;
                default: return null;
            }
        }

        /// <summary>
        /// Create a new array
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object NewArray(ValueTypes type)
        {
            switch (type)
            {
                case ValueTypes.SByte: return new sbyte[] { };
                case ValueTypes.Byte: return new byte[] { };
                case ValueTypes.UShort: return new ushort[] { };
                case ValueTypes.Short: return new short[] { };
                case ValueTypes.UInt: return new uint[] { };
                case ValueTypes.Int: return new int[] { };
                case ValueTypes.ULong: return new ulong[] { };
                case ValueTypes.Long: return new long[] { };
                case ValueTypes.Float: return new float[] { };
                case ValueTypes.Double: return new double[] { };
                case ValueTypes.Char: return new char[] { };
                case ValueTypes.String: return new string[] { };
                case ValueTypes.Bool: return new bool[] { };
                case ValueTypes.Object: return new object[] { };
                default: return null;
            }
        }
        /// <summary>
        /// Create a new array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static object NewArray(ValueTypes type, long size)
        {
            switch (type)
            {
                case ValueTypes.SByte: return new sbyte[size];
                case ValueTypes.Byte: return new byte[size];
                case ValueTypes.UShort: return new ushort[size];
                case ValueTypes.Short: return new short[size];
                case ValueTypes.UInt: return new uint[size];
                case ValueTypes.Int: return new int[size];
                case ValueTypes.ULong: return new ulong[size];
                case ValueTypes.Long: return new long[size];
                case ValueTypes.Float: return new float[size];
                case ValueTypes.Double: return new double[size];
                case ValueTypes.Char: return new char[size];
                case ValueTypes.String: return new string[size];
                case ValueTypes.Bool: return new bool[size];
                case ValueTypes.Object: return new object[size];
                default: return null;
            }
        }
        /// <summary>
        /// Create a new array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <param name="presetValue"></param>
        /// <returns></returns>
        public static object NewArray(ValueTypes type, long size, object presetValue)
        {
            object array = null;
            switch (type)
            {
                case ValueTypes.SByte: array = new sbyte[size]; for (int i = 0; i < ((sbyte[])array).Length; i++) { ((sbyte[])array)[i] = (sbyte)presetValue; } return array;
                case ValueTypes.Byte: array = new byte[size]; for (int i = 0; i < ((byte[])array).Length; i++) { ((byte[])array)[i] = (byte)presetValue; ; } return array;
                case ValueTypes.UShort: array = new ushort[size]; for (int i = 0; i < ((ushort[])array).Length; i++) { ((ushort[])array)[i] = (ushort)presetValue; } return array;
                case ValueTypes.Short: array = new short[size]; for (int i = 0; i < ((short[])array).Length; i++) { ((short[])array)[i] = (short)presetValue; } return array;
                case ValueTypes.UInt: array = new uint[size]; for (int i = 0; i < ((uint[])array).Length; i++) { ((uint[])array)[i] = (uint)presetValue; } return array;
                case ValueTypes.Int: array = new int[size]; for (int i = 0; i < ((int[])array).Length; i++) { ((int[])array)[i] = (int)presetValue; } return array;
                case ValueTypes.ULong: array = new ulong[size]; for (int i = 0; i < ((ulong[])array).Length; i++) { ((ulong[])array)[i] = (ulong)presetValue; } return array;
                case ValueTypes.Long: array = new long[size]; for (int i = 0; i < ((long[])array).Length; i++) { ((long[])array)[i] = (long)presetValue; } return array;
                case ValueTypes.Float: array = new float[size]; for (int i = 0; i < ((float[])array).Length; i++) { ((float[])array)[i] = (float)presetValue; } return array;
                case ValueTypes.Double: array = new double[size]; for (int i = 0; i < ((double[])array).Length; i++) { ((double[])array)[i] = (double)presetValue; } return array;
                case ValueTypes.Char: array = new char[size]; for (int i = 0; i < ((char[])array).Length; i++) { ((char[])array)[i] = (char)presetValue; } return array;
                case ValueTypes.String: array = new string[size]; for (int i = 0; i < ((string[])array).Length; i++) { ((string[])array)[i] = (string)presetValue; } return array;
                case ValueTypes.Bool: array = new bool[size]; for (int i = 0; i < ((bool[])array).Length; i++) { ((bool[])array)[i] = (bool)presetValue; } return array;
                case ValueTypes.Object: array = new object[size]; for (int i = 0; i < ((object[])array).Length; i++) { ((object[])array)[i] = (object)presetValue; } return array;
                default: return null;
            }
        }

        /// <summary>
        /// Create a new jagged array with random values
        /// </summary>
        /// <param name="type"></param>
        /// <param name="rowSize"></param>
        /// <param name="columnSize"></param>
        /// <param name="presetValue"></param>
        /// <returns></returns>
        public static object NewRandomJaggedArray(ValueTypes type, int rowSize, int columnSize)
        {
            Random rnd = new Random();
            object jaggedArray = null;
            switch (type)
            {
                case ValueTypes.SByte: jaggedArray = new sbyte[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Byte: jaggedArray = new byte[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.UShort: jaggedArray = new ushort[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Short: jaggedArray = new short[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.UInt: jaggedArray = new uint[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Int: jaggedArray = new int[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.ULong: jaggedArray = new ulong[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Long: jaggedArray = new long[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Float: jaggedArray = new float[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Double: jaggedArray = new double[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Char: jaggedArray = new char[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.String: jaggedArray = new string[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Bool: jaggedArray = new bool[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                case ValueTypes.Object: jaggedArray = new object[][] { }; for (int i = 0; i < rowSize; i++) { AddArrayToJaggedArray(type, NewRandomArray(type, columnSize), ref jaggedArray); } return jaggedArray;
                default: return null;
            }
        }
        /// <summary>
        /// Create a new array with random values
        /// </summary>
        /// <param name="type"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static object NewRandomArray(ValueTypes type, long size)
        {
            Random rnd = new Random();
            object array = null;
            switch (type)
            {
                case ValueTypes.SByte: array = new sbyte[size]; for (int i = 0; i < ((sbyte[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Byte: array = new byte[size]; for (int i = 0; i < ((byte[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.UShort: array = new ushort[size]; for (int i = 0; i < ((ushort[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Short: array = new short[size]; for (int i = 0; i < ((short[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.UInt: array = new uint[size]; for (int i = 0; i < ((uint[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Int: array = new int[size]; for (int i = 0; i < ((int[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.ULong: array = new ulong[size]; for (int i = 0; i < ((ulong[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Long: array = new long[size]; for (int i = 0; i < ((long[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Float: array = new float[size]; for (int i = 0; i < ((float[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Double: array = new double[size]; for (int i = 0; i < ((double[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Char: array = new char[size]; for (int i = 0; i < ((char[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.String: array = new string[size]; for (int i = 0; i < ((string[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Bool: array = new bool[size]; for (int i = 0; i < ((bool[])array).Length; i++) { InsertValueToArray(type, GetRandomValue(type), i, ref array); } return array;
                case ValueTypes.Object: array = new object[size]; byte process = (byte)rnd.Next(0, 12); for (int i = 0; i < ((object[])array).Length; i++) { InsertValueToArray(type, GetRandomValue((ValueTypes)Enum.Parse(typeof(ValueTypes), $"{process}")), i, ref array); } return array;
                default: return null;
            }

        }
        /// <summary>
        /// Set random depending on the <see cref="ValueTypes"/> value
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRandomValue(ValueTypes type)
        {
            Random rnd = new Random();
            for (int i = 0; i < 8000000; i++) { }
            switch (type)
            {
                case ValueTypes.SByte: return (sbyte)rnd.Next(sbyte.MinValue, sbyte.MaxValue);
                case ValueTypes.Byte: return (byte)rnd.Next(byte.MinValue, byte.MaxValue);
                case ValueTypes.UShort: return (ushort)rnd.Next(ushort.MinValue, ushort.MaxValue);
                case ValueTypes.Short: return (short)rnd.Next(short.MinValue, short.MaxValue);
                case ValueTypes.UInt: return (uint)rnd.Next((int)uint.MinValue, int.MaxValue);
                case ValueTypes.Int: return (int)rnd.Next(int.MinValue, int.MaxValue);
                case ValueTypes.ULong: return (ulong)rnd.Next(int.MinValue, int.MaxValue);
                case ValueTypes.Long: return (long)rnd.Next(int.MinValue, int.MaxValue);
                case ValueTypes.Float: return (float)rnd.NextDouble();
                case ValueTypes.Double: return (double)rnd.NextDouble();
                case ValueTypes.Char: return Convert.ToChar(rnd.Next(char.MinValue, char.MaxValue));
                case ValueTypes.String: char[] c = (char[])NewArray(ValueTypes.Char, rnd.Next(1, 10)); for (int j = 0; j < c.Length; j++) { c[j] = Convert.ToChar(rnd.Next(char.MinValue, char.MaxValue)); } return (object)string.Concat(c);
                case ValueTypes.Bool: return (bool)(rnd.Next(0, 1) == 1);
                default: return null;
            }
        }

        /// <summary>
        /// Convert a value depending on the <see cref="ValueTypes"/> value
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static object ConvertValue(ValueTypes type, object value)
        {
            switch (type)
            {
                case ValueTypes.SByte: return Convert.ToSByte(value);
                case ValueTypes.Byte: return Convert.ToByte(value);
                case ValueTypes.UShort: return Convert.ToUInt16(value);
                case ValueTypes.Short: return Convert.ToInt16(value);
                case ValueTypes.UInt: return Convert.ToUInt32(value);
                case ValueTypes.Int: return Convert.ToInt32(value);
                case ValueTypes.ULong: return Convert.ToUInt64(value);
                case ValueTypes.Long: return Convert.ToInt64(value);
                case ValueTypes.Float: return Convert.ToDouble(value);
                case ValueTypes.Double: return Convert.ToDouble(value);
                case ValueTypes.Char: return Convert.ToChar(value);
                case ValueTypes.String: return Convert.ToString(value);
                case ValueTypes.Bool: return Convert.ToBoolean(value);
                default: return null;
            }
        }

        /// <summary>
        /// Dictionary to get all type relative with the used values from library ArrayLib 
        /// </summary>
        public static Dictionary<Type,ValueTypes> Types
        {
            get
            {
                Dictionary<Type, ValueTypes> types = new Dictionary<Type, ValueTypes>
                {
                    {typeof(System.SByte),ValueTypes.SByte},
                    {typeof(System.Byte),ValueTypes.Byte},
                    {typeof(System.UInt16),ValueTypes.UShort},
                    {typeof(System.Int16),ValueTypes.Short},
                    {typeof(System.UInt32),ValueTypes.UInt},
                    {typeof(System.Int32),ValueTypes.Int},
                    {typeof(System.UInt64),ValueTypes.ULong},
                    {typeof(System.Int64),ValueTypes.Long},
                    {typeof(System.Single),ValueTypes.Float},
                    {typeof(System.Double),ValueTypes.Double},
                    {typeof(System.Char),ValueTypes.Char},
                    {typeof(System.String),ValueTypes.String},
                    {typeof(System.Boolean),ValueTypes.Bool},
                    {typeof(System.Object),ValueTypes.Object},
                };
                return types;
            }
        }

        /// <summary>
        /// <see cref="enum"/> with all types used from library <see cref="ArrayLib"/>
        /// </summary>
        public enum ValueTypes
        {
            SByte,
            Byte,
            UShort,
            Short,
            UInt,
            Int,
            ULong,
            Long,
            Float,
            Double,
            Char,
            String,
            Bool,
            Object
        }
    }

    /// <summary>
    /// This class contains some aritmetic operations, applyied to different type arrays
    /// </summary>
    public static class ArrayAritmeticOperationsLib
    {
        /// <summary>
        /// Generate a identity array with a informed size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static byte[][] IdentityArray(int size)
        {
            object identityArray = ArrayLib.NewJaggedArray(ArrayLib.ValueTypes.Byte, size, size);
            for (int i = 0; i < size; i++) { ArrayLib.InsertValueToJaggedArray(ArrayLib.ValueTypes.Byte, (byte)1, i, i, ref identityArray); }
            return (byte[][])identityArray;
        }

        /// <summary>
        /// Generate a transposed jagged array to a informed jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static Tuple<ValueTypes, object> TransposeArray(ValueTypes type, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibType = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{type.GetHashCode()}");
            int[,] arraySizes = new int[,] { { ArrayLib.GetJaggedArrayRowSize(arrayLibType, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibType, jaggedArray) } };

            object transposedArray = ArrayLib.NewJaggedArray(arrayLibType, arraySizes[0, 1], arraySizes[0, 0]);
            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibType, ArrayLib.GetJaggedArrayValue(arrayLibType, i, j, jaggedArray), j, i, ref transposedArray);
                }
            }
            return new Tuple<ValueTypes, object>(type, transposedArray);
        }


        /// <summary>
        /// Find the inverse of a informed jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static double[][] InverseJaggedArray(ValueTypes type, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibType = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{type.GetHashCode()}");
            int rowSize = ArrayLib.GetJaggedArrayRowSize(arrayLibType, jaggedArray); /*// convenience */

            double[][] result = null;
            if (type == ValueTypes.Double)
            {
                result = (double[][])jaggedArray;
            }
            else
            {
                result = (double[][])ArrayLib.ConvertJaggedArrayToNewJaggedArrayType(arrayLibType, ArrayLib.ValueTypes.Double, jaggedArray);
            }

            double[][] lum = DecomposeJaggedArray(ValueTypes.Double, jaggedArray, out int[] perm, out int toggle);
            if (lum == null) { throw new Exception("Unable to compute the the inverse from that jagged array"); }
            double[] b = new double[rowSize];
            for (int i = 0; i < rowSize; ++i)
            {
                for (int j = 0; j < rowSize; ++j)
                {
                    if (i == perm[j])
                    {
                        b[j] = 1.0;
                    }
                    else
                    {
                        b[j] = 0.0;
                    }
                }
                double[] x = HelperSolve(lum, b);
                for (int j = 0; j < rowSize; ++j)
                {
                    result[j][i] = x[j];
                }
            }

            return result;
        }
        /// <summary>
        /// Calculate the determinant value of a informed jagged array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static double DeterminantJaggedArray(ValueTypes type, object jaggedArray)
        {
            double[][] lum = DecomposeJaggedArray(type, jaggedArray, out int[] perm, out int toggle);
            if (lum == null) { throw new Exception("Unable to compute the determinant"); }
            double result = toggle;
            for (int i = 0; i < lum.Length; ++i)
            {
                result *= lum[i][i];
            }
            return result;
        }
        /// <summary>
        /// Decompose a jagged array using the LUP decompose method
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <param name="permutationArray"></param>
        /// <param name="toggle"></param>
        /// <returns></returns>
        public static double[][] DecomposeJaggedArray(ValueTypes type, object jaggedArray, out int[] permutationArray, out int toggle)
        {
            // Doolittle LUP decomposition.
            // assumes jaggedArray is square.

            ArrayLib.ValueTypes arrayLibType = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{type.GetHashCode()}");
            int rowSize = ArrayLib.GetJaggedArrayRowSize(arrayLibType, jaggedArray); /*// convenience */

            double[][] result = null;
            if (type == ValueTypes.Double)
            {
                result = (double[][])jaggedArray;
            }
            else
            {
                result = (double[][])ArrayLib.ConvertJaggedArrayToNewJaggedArrayType(arrayLibType, ArrayLib.ValueTypes.Double, jaggedArray);
            }
            permutationArray = new int[rowSize];
            for (int i = 0; i < rowSize; ++i)
            {
                permutationArray[i] = i;
            }
            toggle = 1;
            for (int j = 0; j < rowSize - 1; ++j) /*// each column */
            {
                double colMax = Math.Abs(result[j][j]); /*// largest val in col j*/
                int pRow = j;
                for (int i = j + 1; i < rowSize; ++i)
                {
                    if (result[i][j] > colMax)
                    {
                        colMax = result[i][j];
                        pRow = i;
                    }
                }
                if (pRow != j) /*// swap rows*/
                {
                    double[] rowPtr = result[pRow];
                    result[pRow] = result[j];
                    result[j] = rowPtr;
                    int tmp = permutationArray[pRow]; /*// and swap perm info*/
                    permutationArray[pRow] = permutationArray[j];
                    permutationArray[j] = tmp;
                    toggle = -toggle; /*// row-swap toggle*/
                }
                if (Math.Abs(result[j][j]) < 1.0E-20)
                {
                    return null; /*// consider a throw*/
                }
                for (int i = j + 1; i < rowSize; ++i)
                {
                    result[i][j] /= result[j][j];
                    for (int k = j + 1; k < rowSize; ++k)
                    {
                        result[i][k] -= result[i][j] * result[j][k];
                    }
                }
            } /*// main j column loop*/
            return result;
        }
        /// <summary>
        /// Find the answer from a equation between two differents arrays using HelperSolver method
        /// </summary>
        /// <param name="jaggedArrayType"></param>
        /// <param name="jaggedArray"></param>
        /// <param name="arrayType"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static double[] HelperSolve(double[][] jaggedArray, double[] permutationArray)
        {
            /* // solve jaggedArray * x = permutationArray */
            int n = jaggedArray.Length;
            double[] x = new double[n];
            permutationArray.CopyTo(x, 0);
            for (int i = 1; i < n; ++i)
            {
                double sum = x[i];
                for (int j = 0; j < i; ++j)
                {
                    sum -= jaggedArray[i][j] * x[j];
                }
                x[i] = sum;
            }
            x[n - 1] /= jaggedArray[n - 1][n - 1];
            for (int i = n - 2; i >= 0; --i)
            {
                double sum = x[i];
                for (int j = i + 1; j < n; ++j)
                {
                    sum -= jaggedArray[i][j] * x[j];
                }
                x[i] = sum / jaggedArray[i][i];
            }
            return x;
        }


        /// <summary>
        /// Sum two jagged arrays *The number of columns and rows must be equal to number from the other array
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="jaggedArrayA"></param>
        /// <param name="typeB"></param>
        /// <param name="jaggedArrayB"></param>
        /// <returns></returns>
        public static Tuple<ValueTypes, object> SumJaggedArrays(ValueTypes typeA, object jaggedArrayA, ValueTypes typeB, object jaggedArrayB)
        {
            ArrayLib.ValueTypes arrayLibTypeA = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeA.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeB = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeB.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;
                                          
            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeA, jaggedArrayA), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeA, jaggedArrayA) }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeB, jaggedArrayB), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeB, jaggedArrayB) } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            if (typeA.GetHashCode() >= typeB.GetHashCode()) { typeC = typeA; } else { typeC = typeB; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");

            object arrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, SumRealNumbers(typeA, ArrayLib.GetJaggedArrayValue((ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeA.GetHashCode()}"), i, j, jaggedArrayA), typeB, ArrayLib.GetJaggedArrayValue((ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeB.GetHashCode()}"), i, j, jaggedArrayB)), i, j, ref arrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC, arrayC);
        }

        /// <summary>
        /// Subtract two jagged arrays *The number of columns and rows must be equal to number from the other array
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="jaggedArrayA"></param>
        /// <param name="typeB"></param>
        /// <param name="jaggedArrayB"></param>
        /// <returns></returns>
        public static Tuple<ValueTypes, object> SubtractionJaggedArrays(ValueTypes typeA, object jaggedArrayA, ValueTypes typeB, object jaggedArrayB)
        {
            ArrayLib.ValueTypes arrayLibTypeA = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeA.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeB = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeB.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeA, jaggedArrayA), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeA, jaggedArrayA) }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeB, jaggedArrayB), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeB, jaggedArrayB) } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            if (typeA.GetHashCode() >= typeB.GetHashCode()) { typeC = typeA; } else { typeC = typeB; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");

            object arrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, SubtractionRealNumbers(typeA, ArrayLib.GetJaggedArrayValue((ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeA.GetHashCode()}"), i, j, jaggedArrayA), typeB, ArrayLib.GetJaggedArrayValue((ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeB.GetHashCode()}"), i, j, jaggedArrayB)), i, j, ref arrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC, arrayC);
        }

        /// <summary>
        /// Multiply two differents jagged arrays *The number of columns must be equal to number of rows from the other array
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="jaggedArrayA"></param>
        /// <param name="typeB"></param>
        /// <param name="jaggedArrayB"></param>
        /// <returns></returns>
        public static Tuple<ValueTypes, object> MultiplicationJaggedArrays(ValueTypes typeA, object jaggedArrayA, ValueTypes typeB, object jaggedArrayB)
        {
            ArrayLib.ValueTypes arrayLibTypeA = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeA.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeB = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeB.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeA, jaggedArrayA), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeA, jaggedArrayA) }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeB, jaggedArrayB), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeB, jaggedArrayB) } };
            if (!(arraySizes[0, 1] == arraySizes[1, 0])) { return null; }
            if (typeA.GetHashCode() >= typeB.GetHashCode()) { typeC = typeA; } else { typeC = typeB; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");

            object jaggedArrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[0, 0], arraySizes[1, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                object arrayA = ArrayLib.GetJaggedArrayRow(arrayLibTypeA, i, jaggedArrayA);
                for (int j = 0; j < arraySizes[1, 1]; j++)
                {
                    object arrayB = ArrayLib.GetJaggedArrayColumn(arrayLibTypeB, j, jaggedArrayB);
                    object arrayC = ArrayLib.NewArray(arrayLibTypeC, arraySizes[0, 1]);

                    for (int k = 0; k < arraySizes[0, 1]; k++)
                    {
                        ArrayLib.InsertValueToArray(arrayLibTypeC, MultiplicationRealNumbers(typeA, ArrayLib.GetArrayValue(arrayLibTypeA, k, arrayA), typeB, ArrayLib.GetArrayValue(arrayLibTypeB, k, arrayB)), k, ref arrayC);
                    }
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, SumAllElementsFromArray(typeC, typeC, arrayC), i, j, ref jaggedArrayC);
                }
            }


            return new Tuple<ValueTypes, object>(typeC, jaggedArrayC);
        }


        /// <summary>
        /// Sum two arrays *The number of rows must be equal to number from the other array
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="arrayA"></param>
        /// <param name="typeB"></param>
        /// <param name="arrayB"></param>
        /// <returns></returns>
        public static Tuple<ValueTypes, object> SumArrays(ValueTypes typeA, object arrayA, ValueTypes typeB, object arrayB)
        {
            ArrayLib.ValueTypes arrayLibTypeA = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeA.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeB = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeB.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[] arraySizes = new int[] { ArrayLib.GetArrayLenght(arrayLibTypeA, arrayA), ArrayLib.GetArrayLenght(arrayLibTypeB, arrayB) };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            if (typeA.GetHashCode() >= typeB.GetHashCode()) { typeC = typeA; } else { typeC = typeB; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");

            object arrayC = ArrayLib.NewArray(arrayLibTypeC, arraySizes[0]);

            for (int i = 0; i < arraySizes[0]; i++)
            {
                ArrayLib.InsertValueToArray(arrayLibTypeC, SumRealNumbers(typeA, ArrayLib.GetArrayValue(arrayLibTypeA, i, arrayA), typeB, ArrayLib.GetArrayValue(arrayLibTypeB, i, arrayB)), i, ref arrayC);
            }

            return new Tuple<ValueTypes, object>(typeC, arrayC);
        }

        /// <summary>
        /// Subtract two arrays *The number of rows must be equal to number from the other array
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="arrayA"></param>
        /// <param name="typeB"></param>
        /// <param name="arrayB"></param>
        /// <returns></returns>
        public static Tuple<ValueTypes, object> SubtractionArrays(ValueTypes typeA, object arrayA, ValueTypes typeB, object arrayB)
        {
            ArrayLib.ValueTypes arrayLibTypeA = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeA.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeB = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeB.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[] arraySizes = new int[] { ArrayLib.GetArrayLenght(arrayLibTypeA, arrayA), ArrayLib.GetArrayLenght(arrayLibTypeB, arrayB) };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            if (typeA.GetHashCode() >= typeB.GetHashCode()) { typeC = typeA; } else { typeC = typeB; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");

            object arrayC = ArrayLib.NewArray(arrayLibTypeC, arraySizes[0]);
            
            for (int i = 0; i < arraySizes[0]; i++)
            {
                ArrayLib.InsertValueToArray(arrayLibTypeC, SubtractionRealNumbers(typeA, ArrayLib.GetArrayValue(arrayLibTypeA,i, arrayA), typeB, ArrayLib.GetArrayValue(arrayLibTypeB, i, arrayB)),i, ref arrayC);
            }

            return new Tuple<ValueTypes, object>(typeC, arrayC);
        }
              

        /// <summary>
        /// Sum two real values
        /// </summary>
        /// <param name="typeRealValue"></param>
        /// <param name="valueA"></param>
        /// <param name="typeJaggedArray"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static Tuple<ValueTypes, object> SumRealNumberWithJaggedArray(ValueTypes typeRealValue, object realValue, ValueTypes typeJaggedArray, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibTypeRealValue = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeJaggedArray = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { {1,1},{ ArrayLib.GetJaggedArrayRowSize(arrayLibTypeJaggedArray, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeJaggedArray, jaggedArray) } };            
            if (typeRealValue.GetHashCode() >= typeJaggedArray.GetHashCode()) { typeC = typeRealValue; } else { typeC = typeJaggedArray; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");
            object jaggedArrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[1, 1]);

            for(int i = 0; i<arraySizes[1,0];i++)
            {
                for (int j = 0; j < arraySizes[1, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, SumRealNumbers(typeRealValue,realValue,typeJaggedArray,ArrayLib.GetJaggedArrayValue(arrayLibTypeJaggedArray,i,j,jaggedArray)), i, j, ref jaggedArrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC,jaggedArrayC);
        }

        /// <summary>
        /// Subtract two real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static Tuple<ValueTypes, object> SubtractionRealNumberWithJaggedArray(ValueTypes typeRealValue, object realValue, ValueTypes typeJaggedArray, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibTypeRealValue = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeJaggedArray = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { 1, 1 }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeJaggedArray, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeJaggedArray, jaggedArray) } };
            if (typeRealValue.GetHashCode() >= typeJaggedArray.GetHashCode()) { typeC = typeRealValue; } else { typeC = typeJaggedArray; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");
            object jaggedArrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[1, 1]);

            for (int i = 0; i < arraySizes[1, 0]; i++)
            {
                for (int j = 0; j < arraySizes[1, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, SubtractionRealNumbers(typeRealValue, realValue, typeJaggedArray, ArrayLib.GetJaggedArrayValue(arrayLibTypeJaggedArray, i, j, jaggedArray)), i, j, ref jaggedArrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC, jaggedArrayC);
        }

        /// <summary>
        /// Multiply two real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static Tuple<ValueTypes, object> MultiplicationRealNumberWithJaggedArray(ValueTypes typeRealValue, object realValue, ValueTypes typeJaggedArray, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibTypeRealValue = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeJaggedArray = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { 1, 1 }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeJaggedArray, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeJaggedArray, jaggedArray) } };
            if (typeRealValue.GetHashCode() >= typeJaggedArray.GetHashCode()) { typeC = typeRealValue; } else { typeC = typeJaggedArray; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");
            object jaggedArrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[1, 1]);

            for (int i = 0; i < arraySizes[1, 0]; i++)
            {
                for (int j = 0; j < arraySizes[1, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, MultiplicationRealNumbers(typeRealValue, realValue, typeJaggedArray, ArrayLib.GetJaggedArrayValue(arrayLibTypeJaggedArray, i, j, jaggedArray)), i, j, ref jaggedArrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC, jaggedArrayC);
        }

        /// <summary>
        /// Split between two real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static Tuple<ValueTypes, object> SplitRealNumberWithJaggedArray(ValueTypes typeRealValue, object realValue, ValueTypes typeJaggedArray, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibTypeRealValue = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeJaggedArray = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { 1, 1 }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeJaggedArray, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeJaggedArray, jaggedArray) } };
            if (typeRealValue.GetHashCode() >= typeJaggedArray.GetHashCode()) { typeC = typeRealValue; } else { typeC = typeJaggedArray; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");
            object jaggedArrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[1, 1]);

            for (int i = 0; i < arraySizes[1, 0]; i++)
            {
                for (int j = 0; j < arraySizes[1, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, SplitRealNumbers(typeRealValue, realValue, typeJaggedArray, ArrayLib.GetJaggedArrayValue(arrayLibTypeJaggedArray, i, j, jaggedArray)), i, j, ref jaggedArrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC, jaggedArrayC);
        }

        /// <summary>
        /// Remaining split between real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static Tuple<ValueTypes, object> RemainingSplitRealNumberWithJaggedArray(ValueTypes typeRealValue, object realValue, ValueTypes typeJaggedArray, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibTypeRealValue = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeJaggedArray = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { 1, 1 }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeJaggedArray, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeJaggedArray, jaggedArray) } };
            if (typeRealValue.GetHashCode() >= typeJaggedArray.GetHashCode()) { typeC = typeRealValue; } else { typeC = typeJaggedArray; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");
            object jaggedArrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[1, 1]);

            for (int i = 0; i < arraySizes[1, 0]; i++)
            {
                for (int j = 0; j < arraySizes[1, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, RemainingSplitRealNumbers(typeRealValue, realValue, typeJaggedArray, ArrayLib.GetJaggedArrayValue(arrayLibTypeJaggedArray, i, j, jaggedArray)), i, j, ref jaggedArrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC, jaggedArrayC);
        }

        /// <summary>
        /// Power between two  real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static Tuple<ValueTypes, object> PowerRealNumberInJaggedArray(ValueTypes typeRealValue, object realValue, ValueTypes typeJaggedArray, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibTypeRealValue = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeJaggedArray = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeJaggedArray.GetHashCode()}");
            ArrayLib.ValueTypes arrayLibTypeC;

            ValueTypes typeC;

            int[,] arraySizes = new int[,] { { 1, 1 }, { ArrayLib.GetJaggedArrayRowSize(arrayLibTypeJaggedArray, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibTypeJaggedArray, jaggedArray) } };
            if (typeRealValue.GetHashCode() >= typeJaggedArray.GetHashCode()) { typeC = typeRealValue; } else { typeC = typeJaggedArray; }
            arrayLibTypeC = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{typeC.GetHashCode()}");
            object jaggedArrayC = ArrayLib.NewJaggedArray(arrayLibTypeC, arraySizes[1, 0], arraySizes[1, 1]);

            for (int i = 0; i < arraySizes[1, 0]; i++)
            {
                for (int j = 0; j < arraySizes[1, 1]; j++)
                {
                    ArrayLib.InsertValueToJaggedArray(arrayLibTypeC, PowerRealNumbers(typeRealValue, realValue, typeJaggedArray, ArrayLib.GetJaggedArrayValue(arrayLibTypeJaggedArray, i, j, jaggedArray)), i, j, ref jaggedArrayC);
                }
            }
            return new Tuple<ValueTypes, object>(typeC, jaggedArrayC);
        }
                          
        /// <summary>
        /// Sum two real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static object SumRealNumbers(ValueTypes typeA, object valueA, ValueTypes typeB, object valueB)
        {
            sbyte? sbyteA = null, sbyteB = null; byte? byteA = null, byteB = null; ushort? ushortA = null, ushortB = null; short? shortA = null, shortB = null; uint? uintA = null, uintB = null; int? intA = null, intB = null; ulong? ulongA = null, ulongB = null; long? longA = null, longB = null; float? floatA = null, floatB = null; double? doubleA = null, doubleB = null;
            switch (typeA)
            {
                case ValueTypes.SByte: sbyteA = (sbyte)valueA; break;
                case ValueTypes.Byte: byteA = (byte)valueA; break;
                case ValueTypes.UShort: ushortA = (ushort)valueA; break;
                case ValueTypes.Short: shortA = (short)valueA; break;
                case ValueTypes.UInt: uintA = (uint)valueA; break;
                case ValueTypes.Int: intA = (int)valueA; break;
                case ValueTypes.ULong: ulongA = (ulong)valueA; break;
                case ValueTypes.Long: longA = (long)valueA; break;
                case ValueTypes.Float: floatA = (float)valueA; break;
                case ValueTypes.Double: doubleA = (double)valueA; break;
            }
            switch (typeB)
            {
                case ValueTypes.SByte: sbyteB = (sbyte)valueB; break;
                case ValueTypes.Byte: byteB = (byte)valueB; break;
                case ValueTypes.UShort: ushortB = (ushort)valueB; break;
                case ValueTypes.Short: shortB = (short)valueB; break;
                case ValueTypes.UInt: uintB = (uint)valueB; break;
                case ValueTypes.Int: intB = (int)valueB; break;
                case ValueTypes.ULong: ulongB = (ulong)valueB; break;
                case ValueTypes.Long: longB = (long)valueB; break;
                case ValueTypes.Float: floatB = (float)valueB; break;
                case ValueTypes.Double: doubleB = (double)valueB; break;
            }
            switch (typeA)
            {
                case ValueTypes.SByte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((sbyte?)sbyteA + (sbyte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)sbyteA + (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)sbyteA + (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)sbyteA + (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)sbyteA + (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)sbyteA + (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)sbyteA + (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)sbyteA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)sbyteA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)sbyteA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Byte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((byte?)byteA + (byte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)byteA + (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)byteA + (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)byteA + (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)byteA + (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)byteA + (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)byteA + (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)byteA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)byteA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)byteA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UShort:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ushort?)ushortA + (ushort?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ushort?)ushortA + (ushort?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)ushortA + (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)ushortA + (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)ushortA + (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)ushortA + (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ushortA + (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ushortA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ushortA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ushortA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Short:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((short?)shortA + (short?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((short?)shortA + (short?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((short?)shortA + (short?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)shortA + (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)shortA + (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)shortA + (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)shortA + (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)shortA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)shortA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)shortA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UInt:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((uint?)uintA + (uint?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((uint?)uintA + (uint?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((uint?)uintA + (uint?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((uint?)uintA + (uint?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)uintA + (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)uintA + (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)uintA + (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)uintA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)uintA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)uintA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Int:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((int?)intA + (int?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((int?)intA + (int?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((int?)intA + (int?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((int?)intA + (int?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((int?)intA + (int?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)intA + (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)intA + (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)intA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)intA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)intA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.ULong:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ulong?)ulongA + (ulong?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ulong?)ulongA + (ulong?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ulong?)ulongA + (ulong?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((ulong?)ulongA + (ulong?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((ulong?)ulongA + (ulong?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((ulong?)ulongA + (ulong?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ulongA + (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ulongA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ulongA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ulongA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Long:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((long?)longA + (long?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((long?)longA + (long?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((long?)longA + (long?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((long?)longA + (long?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((long?)longA + (long?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((long?)longA + (long?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((long?)longA + (long?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)longA + (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)longA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)longA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Float:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((float?)floatA + (float?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((float?)floatA + (float?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((float?)floatA + (float?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((float?)floatA + (float?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((float?)floatA + (float?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((float?)floatA + (float?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((float?)floatA + (float?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((float?)floatA + (float?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)floatA + (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)floatA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Double:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((double?)doubleA + (double?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((double?)doubleA + (double?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((double?)doubleA + (double?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((double?)doubleA + (double?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((double?)doubleA + (double?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((double?)doubleA + (double?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((double?)doubleA + (double?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((double?)doubleA + (double?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((double?)doubleA + (double?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)doubleA + (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                default: return null;
            }
        }

        /// <summary>
        /// Subtract two real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static object SubtractionRealNumbers(ValueTypes typeA, object valueA, ValueTypes typeB, object valueB)
        {
            sbyte? sbyteA = null, sbyteB = null; byte? byteA = null, byteB = null; ushort? ushortA = null, ushortB = null; short? shortA = null, shortB = null; uint? uintA = null, uintB = null; int? intA = null, intB = null; ulong? ulongA = null, ulongB = null; long? longA = null, longB = null; float? floatA = null, floatB = null; double? doubleA = null, doubleB = null;
            switch (typeA)
            {
                case ValueTypes.SByte: sbyteA = (sbyte)valueA; break;
                case ValueTypes.Byte: byteA = (byte)valueA; break;
                case ValueTypes.UShort: ushortA = (ushort)valueA; break;
                case ValueTypes.Short: shortA = (short)valueA; break;
                case ValueTypes.UInt: uintA = (uint)valueA; break;
                case ValueTypes.Int: intA = (int)valueA; break;
                case ValueTypes.ULong: ulongA = (ulong)valueA; break;
                case ValueTypes.Long: longA = (long)valueA; break;
                case ValueTypes.Float: floatA = (float)valueA; break;
                case ValueTypes.Double: doubleA = (double)valueA; break;
            }
            switch (typeB)
            {
                case ValueTypes.SByte: sbyteB = (sbyte)valueB; break;
                case ValueTypes.Byte: byteB = (byte)valueB; break;
                case ValueTypes.UShort: ushortB = (ushort)valueB; break;
                case ValueTypes.Short: shortB = (short)valueB; break;
                case ValueTypes.UInt: uintB = (uint)valueB; break;
                case ValueTypes.Int: intB = (int)valueB; break;
                case ValueTypes.ULong: ulongB = (ulong)valueB; break;
                case ValueTypes.Long: longB = (long)valueB; break;
                case ValueTypes.Float: floatB = (float)valueB; break;
                case ValueTypes.Double: doubleB = (double)valueB; break;
            }
            switch (typeA)
            {
                case ValueTypes.SByte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((sbyte?)sbyteA - (sbyte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)sbyteA - (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)sbyteA - (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)sbyteA - (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)sbyteA - (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)sbyteA - (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)sbyteA - (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)sbyteA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)sbyteA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)sbyteA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Byte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((byte?)byteA - (byte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)byteA - (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)byteA - (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)byteA - (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)byteA - (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)byteA - (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)byteA - (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)byteA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)byteA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)byteA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UShort:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ushort?)ushortA - (ushort?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ushort?)ushortA - (ushort?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)ushortA - (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)ushortA - (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)ushortA - (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)ushortA - (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ushortA - (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ushortA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ushortA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ushortA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Short:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((short?)shortA - (short?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((short?)shortA - (short?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((short?)shortA - (short?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)shortA - (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)shortA - (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)shortA - (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)shortA - (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)shortA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)shortA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)shortA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UInt:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((uint?)uintA - (uint?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((uint?)uintA - (uint?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((uint?)uintA - (uint?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((uint?)uintA - (uint?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)uintA - (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)uintA - (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)uintA - (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)uintA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)uintA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)uintA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Int:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((int?)intA - (int?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((int?)intA - (int?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((int?)intA - (int?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((int?)intA - (int?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((int?)intA - (int?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)intA - (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)intA - (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)intA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)intA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)intA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.ULong:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ulong?)ulongA - (ulong?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ulong?)ulongA - (ulong?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ulong?)ulongA - (ulong?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((ulong?)ulongA - (ulong?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((ulong?)ulongA - (ulong?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((ulong?)ulongA - (ulong?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ulongA - (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ulongA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ulongA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ulongA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Long:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((long?)longA - (long?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((long?)longA - (long?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((long?)longA - (long?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((long?)longA - (long?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((long?)longA - (long?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((long?)longA - (long?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((long?)longA - (long?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)longA - (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)longA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)longA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Float:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((float?)floatA - (float?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((float?)floatA - (float?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((float?)floatA - (float?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((float?)floatA - (float?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((float?)floatA - (float?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((float?)floatA - (float?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((float?)floatA - (float?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((float?)floatA - (float?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)floatA - (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)floatA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Double:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((double?)doubleA - (double?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((double?)doubleA - (double?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((double?)doubleA - (double?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((double?)doubleA - (double?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((double?)doubleA - (double?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((double?)doubleA - (double?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((double?)doubleA - (double?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((double?)doubleA - (double?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((double?)doubleA - (double?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)doubleA - (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                default: return null;
            }
        }

        /// <summary>
        /// Multiply two real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static object MultiplicationRealNumbers(ValueTypes typeA, object valueA, ValueTypes typeB, object valueB)
        {
            sbyte? sbyteA = null, sbyteB = null; byte? byteA = null, byteB = null; ushort? ushortA = null, ushortB = null; short? shortA = null, shortB = null; uint? uintA = null, uintB = null; int? intA = null, intB = null; ulong? ulongA = null, ulongB = null; long? longA = null, longB = null; float? floatA = null, floatB = null; double? doubleA = null, doubleB = null;
            switch (typeA)
            {
                case ValueTypes.SByte: sbyteA = (sbyte)valueA; break;
                case ValueTypes.Byte: byteA = (byte)valueA; break;
                case ValueTypes.UShort: ushortA = (ushort)valueA; break;
                case ValueTypes.Short: shortA = (short)valueA; break;
                case ValueTypes.UInt: uintA = (uint)valueA; break;
                case ValueTypes.Int: intA = (int)valueA; break;
                case ValueTypes.ULong: ulongA = (ulong)valueA; break;
                case ValueTypes.Long: longA = (long)valueA; break;
                case ValueTypes.Float: floatA = (float)valueA; break;
                case ValueTypes.Double: doubleA = (double)valueA; break;
            }
            switch (typeB)
            {
                case ValueTypes.SByte: sbyteB = (sbyte)valueB; break;
                case ValueTypes.Byte: byteB = (byte)valueB; break;
                case ValueTypes.UShort: ushortB = (ushort)valueB; break;
                case ValueTypes.Short: shortB = (short)valueB; break;
                case ValueTypes.UInt: uintB = (uint)valueB; break;
                case ValueTypes.Int: intB = (int)valueB; break;
                case ValueTypes.ULong: ulongB = (ulong)valueB; break;
                case ValueTypes.Long: longB = (long)valueB; break;
                case ValueTypes.Float: floatB = (float)valueB; break;
                case ValueTypes.Double: doubleB = (double)valueB; break;
            }
            switch (typeA)
            {
                case ValueTypes.SByte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((sbyte?)sbyteA * (sbyte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)sbyteA * (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)sbyteA * (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)sbyteA * (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)sbyteA * (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)sbyteA * (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)sbyteA * (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)sbyteA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)sbyteA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)sbyteA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Byte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((byte?)byteA * (byte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)byteA * (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)byteA * (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)byteA * (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)byteA * (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)byteA * (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)byteA * (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)byteA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)byteA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)byteA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UShort:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ushort?)ushortA * (ushort?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ushort?)ushortA * (ushort?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)ushortA * (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)ushortA * (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)ushortA * (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)ushortA * (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ushortA * (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ushortA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ushortA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ushortA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Short:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((short?)shortA * (short?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((short?)shortA * (short?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((short?)shortA * (short?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)shortA * (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)shortA * (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)shortA * (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)shortA * (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)shortA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)shortA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)shortA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UInt:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((uint?)uintA * (uint?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((uint?)uintA * (uint?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((uint?)uintA * (uint?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((uint?)uintA * (uint?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)uintA * (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)uintA * (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)uintA * (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)uintA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)uintA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)uintA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Int:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((int?)intA * (int?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((int?)intA * (int?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((int?)intA * (int?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((int?)intA * (int?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((int?)intA * (int?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)intA * (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)intA * (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)intA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)intA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)intA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.ULong:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ulong?)ulongA * (ulong?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ulong?)ulongA * (ulong?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ulong?)ulongA * (ulong?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((ulong?)ulongA * (ulong?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((ulong?)ulongA * (ulong?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((ulong?)ulongA * (ulong?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ulongA * (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ulongA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ulongA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ulongA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Long:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((long?)longA * (long?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((long?)longA * (long?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((long?)longA * (long?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((long?)longA * (long?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((long?)longA * (long?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((long?)longA * (long?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((long?)longA * (long?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)longA * (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)longA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)longA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Float:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((float?)floatA * (float?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((float?)floatA * (float?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((float?)floatA * (float?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((float?)floatA * (float?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((float?)floatA * (float?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((float?)floatA * (float?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((float?)floatA * (float?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((float?)floatA * (float?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)floatA * (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)floatA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Double:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((double?)doubleA * (double?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((double?)doubleA * (double?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((double?)doubleA * (double?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((double?)doubleA * (double?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((double?)doubleA * (double?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((double?)doubleA * (double?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((double?)doubleA * (double?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((double?)doubleA * (double?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((double?)doubleA * (double?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)doubleA * (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                default: return null;
            }
        }

        /// <summary>
        /// Split between two real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static object SplitRealNumbers(ValueTypes typeA, object valueA, ValueTypes typeB, object valueB)
        {
            sbyte? sbyteA = null, sbyteB = null; byte? byteA = null, byteB = null; ushort? ushortA = null, ushortB = null; short? shortA = null, shortB = null; uint? uintA = null, uintB = null; int? intA = null, intB = null; ulong? ulongA = null, ulongB = null; long? longA = null, longB = null; float? floatA = null, floatB = null; double? doubleA = null, doubleB = null;
            switch (typeA)
            {
                case ValueTypes.SByte: sbyteA = (sbyte)valueA; break;
                case ValueTypes.Byte: byteA = (byte)valueA; break;
                case ValueTypes.UShort: ushortA = (ushort)valueA; break;
                case ValueTypes.Short: shortA = (short)valueA; break;
                case ValueTypes.UInt: uintA = (uint)valueA; break;
                case ValueTypes.Int: intA = (int)valueA; break;
                case ValueTypes.ULong: ulongA = (ulong)valueA; break;
                case ValueTypes.Long: longA = (long)valueA; break;
                case ValueTypes.Float: floatA = (float)valueA; break;
                case ValueTypes.Double: doubleA = (double)valueA; break;
            }
            switch (typeB)
            {
                case ValueTypes.SByte: sbyteB = (sbyte)valueB; break;
                case ValueTypes.Byte: byteB = (byte)valueB; break;
                case ValueTypes.UShort: ushortB = (ushort)valueB; break;
                case ValueTypes.Short: shortB = (short)valueB; break;
                case ValueTypes.UInt: uintB = (uint)valueB; break;
                case ValueTypes.Int: intB = (int)valueB; break;
                case ValueTypes.ULong: ulongB = (ulong)valueB; break;
                case ValueTypes.Long: longB = (long)valueB; break;
                case ValueTypes.Float: floatB = (float)valueB; break;
                case ValueTypes.Double: doubleB = (double)valueB; break;
            }
            switch (typeA)
            {
                case ValueTypes.SByte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((sbyte?)sbyteA / (sbyte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)sbyteA / (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)sbyteA / (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)sbyteA / (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)sbyteA / (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)sbyteA / (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)sbyteA / (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)sbyteA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)sbyteA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)sbyteA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Byte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((byte?)byteA / (byte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)byteA / (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)byteA / (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)byteA / (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)byteA / (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)byteA / (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)byteA / (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)byteA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)byteA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)byteA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UShort:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ushort?)ushortA / (ushort?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ushort?)ushortA / (ushort?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)ushortA / (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)ushortA / (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)ushortA / (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)ushortA / (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ushortA / (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ushortA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ushortA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ushortA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Short:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((short?)shortA / (short?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((short?)shortA / (short?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((short?)shortA / (short?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)shortA / (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)shortA / (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)shortA / (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)shortA / (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)shortA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)shortA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)shortA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UInt:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((uint?)uintA / (uint?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((uint?)uintA / (uint?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((uint?)uintA / (uint?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((uint?)uintA / (uint?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)uintA / (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)uintA / (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)uintA / (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)uintA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)uintA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)uintA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Int:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((int?)intA / (int?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((int?)intA / (int?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((int?)intA / (int?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((int?)intA / (int?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((int?)intA / (int?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)intA / (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)intA / (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)intA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)intA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)intA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.ULong:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ulong?)ulongA / (ulong?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ulong?)ulongA / (ulong?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ulong?)ulongA / (ulong?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((ulong?)ulongA / (ulong?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((ulong?)ulongA / (ulong?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((ulong?)ulongA / (ulong?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ulongA / (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ulongA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ulongA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ulongA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Long:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((long?)longA / (long?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((long?)longA / (long?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((long?)longA / (long?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((long?)longA / (long?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((long?)longA / (long?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((long?)longA / (long?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((long?)longA / (long?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)longA / (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)longA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)longA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Float:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((float?)floatA / (float?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((float?)floatA / (float?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((float?)floatA / (float?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((float?)floatA / (float?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((float?)floatA / (float?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((float?)floatA / (float?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((float?)floatA / (float?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((float?)floatA / (float?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)floatA / (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)floatA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Double:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((double?)doubleA / (double?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((double?)doubleA / (double?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((double?)doubleA / (double?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((double?)doubleA / (double?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((double?)doubleA / (double?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((double?)doubleA / (double?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((double?)doubleA / (double?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((double?)doubleA / (double?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((double?)doubleA / (double?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)doubleA / (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                default: return null;
            }
        }

        /// <summary>
        /// Remaining split between real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static object RemainingSplitRealNumbers(ValueTypes typeA, object valueA, ValueTypes typeB, object valueB)
        {
            sbyte? sbyteA = null, sbyteB = null; byte? byteA = null, byteB = null; ushort? ushortA = null, ushortB = null; short? shortA = null, shortB = null; uint? uintA = null, uintB = null; int? intA = null, intB = null; ulong? ulongA = null, ulongB = null; long? longA = null, longB = null; float? floatA = null, floatB = null; double? doubleA = null, doubleB = null;
            switch (typeA)
            {
                case ValueTypes.SByte: sbyteA = (sbyte)valueA; break;
                case ValueTypes.Byte: byteA = (byte)valueA; break;
                case ValueTypes.UShort: ushortA = (ushort)valueA; break;
                case ValueTypes.Short: shortA = (short)valueA; break;
                case ValueTypes.UInt: uintA = (uint)valueA; break;
                case ValueTypes.Int: intA = (int)valueA; break;
                case ValueTypes.ULong: ulongA = (ulong)valueA; break;
                case ValueTypes.Long: longA = (long)valueA; break;
                case ValueTypes.Float: floatA = (float)valueA; break;
                case ValueTypes.Double: doubleA = (double)valueA; break;
            }
            switch (typeB)
            {
                case ValueTypes.SByte: sbyteB = (sbyte)valueB; break;
                case ValueTypes.Byte: byteB = (byte)valueB; break;
                case ValueTypes.UShort: ushortB = (ushort)valueB; break;
                case ValueTypes.Short: shortB = (short)valueB; break;
                case ValueTypes.UInt: uintB = (uint)valueB; break;
                case ValueTypes.Int: intB = (int)valueB; break;
                case ValueTypes.ULong: ulongB = (ulong)valueB; break;
                case ValueTypes.Long: longB = (long)valueB; break;
                case ValueTypes.Float: floatB = (float)valueB; break;
                case ValueTypes.Double: doubleB = (double)valueB; break;
            }
            switch (typeA)
            {
                case ValueTypes.SByte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((sbyte?)sbyteA % (sbyte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)sbyteA % (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)sbyteA % (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)sbyteA % (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)sbyteA % (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)sbyteA % (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)sbyteA % (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)sbyteA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)sbyteA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)sbyteA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Byte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((byte?)byteA % (byte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)byteA % (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)byteA % (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)byteA % (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)byteA % (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)byteA % (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)byteA % (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)byteA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)byteA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)byteA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UShort:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ushort?)ushortA % (ushort?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ushort?)ushortA % (ushort?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)ushortA % (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)ushortA % (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)ushortA % (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)ushortA % (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ushortA % (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ushortA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ushortA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ushortA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Short:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((short?)shortA % (short?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((short?)shortA % (short?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((short?)shortA % (short?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)shortA % (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)shortA % (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)shortA % (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)shortA % (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)shortA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)shortA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)shortA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.UInt:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((uint?)uintA % (uint?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((uint?)uintA % (uint?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((uint?)uintA % (uint?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((uint?)uintA % (uint?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)uintA % (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)uintA % (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)uintA % (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)uintA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)uintA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)uintA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Int:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((int?)intA % (int?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((int?)intA % (int?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((int?)intA % (int?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((int?)intA % (int?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((int?)intA % (int?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)intA % (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)intA % (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)intA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)intA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)intA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.ULong:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ulong?)ulongA % (ulong?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ulong?)ulongA % (ulong?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ulong?)ulongA % (ulong?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((ulong?)ulongA % (ulong?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((ulong?)ulongA % (ulong?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((ulong?)ulongA % (ulong?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ulongA % (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ulongA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)ulongA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)ulongA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Long:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((long?)longA % (long?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((long?)longA % (long?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((long?)longA % (long?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((long?)longA % (long?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((long?)longA % (long?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((long?)longA % (long?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((long?)longA % (long?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)longA % (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)longA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)longA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Float:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((float?)floatA % (float?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((float?)floatA % (float?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((float?)floatA % (float?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((float?)floatA % (float?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((float?)floatA % (float?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((float?)floatA % (float?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((float?)floatA % (float?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((float?)floatA % (float?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((float?)floatA % (float?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)floatA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                case ValueTypes.Double:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((double?)doubleA % (double?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((double?)doubleA % (double?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((double?)doubleA % (double?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((double?)doubleA % (double?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((double?)doubleA % (double?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((double?)doubleA % (double?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((double?)doubleA % (double?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((double?)doubleA % (double?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return ((double?)doubleA % (double?)floatB).GetValueOrDefault();
                        case ValueTypes.Double: return ((double?)doubleA % (double?)doubleB).GetValueOrDefault();
                        default: return null;
                    }
                default: return null;
            }
        }

        /// <summary>
        /// Power between two  real values
        /// </summary>
        /// <param name="typeA"></param>
        /// <param name="valueA"></param>
        /// <param name="typeB"></param>
        /// <param name="valueB"></param>
        /// <returns></returns>
        static object PowerRealNumbers(ValueTypes typeA, object valueA, ValueTypes typeB, object valueB)
        {
            sbyte? sbyteA = null, sbyteB = null; byte? byteA = null, byteB = null; ushort? ushortA = null, ushortB = null; short? shortA = null, shortB = null; uint? uintA = null, uintB = null; int? intA = null, intB = null; ulong? ulongA = null, ulongB = null; long? longA = null, longB = null; float? floatA = null, floatB = null; double? doubleA = null, doubleB = null;
            switch (typeA)
            {
                case ValueTypes.SByte: sbyteA = (sbyte)valueA; break;
                case ValueTypes.Byte: byteA = (byte)valueA; break;
                case ValueTypes.UShort: ushortA = (ushort)valueA; break;
                case ValueTypes.Short: shortA = (short)valueA; break;
                case ValueTypes.UInt: uintA = (uint)valueA; break;
                case ValueTypes.Int: intA = (int)valueA; break;
                case ValueTypes.ULong: ulongA = (ulong)valueA; break;
                case ValueTypes.Long: longA = (long)valueA; break;
                case ValueTypes.Float: floatA = (float)valueA; break;
                case ValueTypes.Double: doubleA = (double)valueA; break;
            }
            switch (typeB)
            {
                case ValueTypes.SByte: sbyteB = (sbyte)valueB; break;
                case ValueTypes.Byte: byteB = (byte)valueB; break;
                case ValueTypes.UShort: ushortB = (ushort)valueB; break;
                case ValueTypes.Short: shortB = (short)valueB; break;
                case ValueTypes.UInt: uintB = (uint)valueB; break;
                case ValueTypes.Int: intB = (int)valueB; break;
                case ValueTypes.ULong: ulongB = (ulong)valueB; break;
                case ValueTypes.Long: longB = (long)valueB; break;
                case ValueTypes.Float: floatB = (float)valueB; break;
                case ValueTypes.Double: doubleB = (double)valueB; break;
            }
            switch (typeA)
            {
                case ValueTypes.SByte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((sbyte?)sbyteA ^ (sbyte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)sbyteA ^ (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)sbyteA ^ (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)sbyteA ^ (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)sbyteA ^ (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)sbyteA ^ (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)sbyteA ^ (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)sbyteA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(sbyteA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(sbyteA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.Byte:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((byte?)byteA ^ (byte?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((byte?)byteA ^ (byte?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)byteA ^ (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)byteA ^ (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)byteA ^ (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)byteA ^ (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)byteA ^ (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)byteA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(byteA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(byteA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.UShort:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ushort?)ushortA ^ (ushort?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ushort?)ushortA ^ (ushort?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ushort?)ushortA ^ (ushort?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)ushortA ^ (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)ushortA ^ (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)ushortA ^ (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ushortA ^ (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ushortA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(ushortA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(ushortA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.Short:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((short?)shortA ^ (short?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((short?)shortA ^ (short?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((short?)shortA ^ (short?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((short?)shortA ^ (short?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)shortA ^ (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)shortA ^ (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)shortA ^ (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)shortA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(shortA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(shortA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.UInt:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((uint?)uintA ^ (uint?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((uint?)uintA ^ (uint?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((uint?)uintA ^ (uint?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((uint?)uintA ^ (uint?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((uint?)uintA ^ (uint?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)uintA ^ (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)uintA ^ (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)uintA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(uintA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(uintA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.Int:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((int?)intA ^ (int?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((int?)intA ^ (int?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((int?)intA ^ (int?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((int?)intA ^ (int?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((int?)intA ^ (int?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((int?)intA ^ (int?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)intA ^ (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)intA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(intA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(intA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.ULong:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((ulong?)ulongA ^ (ulong?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((ulong?)ulongA ^ (ulong?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((ulong?)ulongA ^ (ulong?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((ulong?)ulongA ^ (ulong?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((ulong?)ulongA ^ (ulong?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((ulong?)ulongA ^ (ulong?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((ulong?)ulongA ^ (ulong?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)ulongA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(ulongA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(ulongA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.Long:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return ((long?)longA ^ (long?)sbyteB).GetValueOrDefault();
                        case ValueTypes.Byte: return ((long?)longA ^ (long?)byteB).GetValueOrDefault();
                        case ValueTypes.UShort: return ((long?)longA ^ (long?)ushortB).GetValueOrDefault();
                        case ValueTypes.Short: return ((long?)longA ^ (long?)shortB).GetValueOrDefault();
                        case ValueTypes.UInt: return ((long?)longA ^ (long?)uintB).GetValueOrDefault();
                        case ValueTypes.Int: return ((long?)longA ^ (long?)intB).GetValueOrDefault();
                        case ValueTypes.ULong: return ((long?)longA ^ (long?)ulongB).GetValueOrDefault();
                        case ValueTypes.Long: return ((long?)longA ^ (long?)longB).GetValueOrDefault();
                        case ValueTypes.Float: return (float)Math.Pow(longA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(longA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.Float:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return (float)Math.Pow(floatA.GetValueOrDefault(), sbyteB.GetValueOrDefault());
                        case ValueTypes.Byte: return (float)Math.Pow(floatA.GetValueOrDefault(), byteB.GetValueOrDefault());
                        case ValueTypes.UShort: return (float)Math.Pow(floatA.GetValueOrDefault(), ushortB.GetValueOrDefault());
                        case ValueTypes.Short: return (float)Math.Pow(floatA.GetValueOrDefault(), shortB.GetValueOrDefault());
                        case ValueTypes.UInt: return (float)Math.Pow(floatA.GetValueOrDefault(), uintB.GetValueOrDefault());
                        case ValueTypes.Int: return (float)Math.Pow(floatA.GetValueOrDefault(), intB.GetValueOrDefault());
                        case ValueTypes.ULong: return (float)Math.Pow(floatA.GetValueOrDefault(), ulongB.GetValueOrDefault());
                        case ValueTypes.Long: return (float)Math.Pow(floatA.GetValueOrDefault(), longB.GetValueOrDefault());
                        case ValueTypes.Float: return (float)Math.Pow(floatA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(floatA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                case ValueTypes.Double:
                    switch (typeB)
                    {
                        case ValueTypes.SByte: return (double)Math.Pow(doubleA.GetValueOrDefault(), sbyteB.GetValueOrDefault());
                        case ValueTypes.Byte: return (double)Math.Pow(doubleA.GetValueOrDefault(), byteB.GetValueOrDefault());
                        case ValueTypes.UShort: return (double)Math.Pow(doubleA.GetValueOrDefault(), ushortB.GetValueOrDefault());
                        case ValueTypes.Short: return (double)Math.Pow(doubleA.GetValueOrDefault(), shortB.GetValueOrDefault());
                        case ValueTypes.UInt: return (double)Math.Pow(doubleA.GetValueOrDefault(), uintB.GetValueOrDefault());
                        case ValueTypes.Int: return (double)Math.Pow(doubleA.GetValueOrDefault(), intB.GetValueOrDefault());
                        case ValueTypes.ULong: return (double)Math.Pow(doubleA.GetValueOrDefault(), ulongB.GetValueOrDefault());
                        case ValueTypes.Long: return (double)Math.Pow(doubleA.GetValueOrDefault(), longB.GetValueOrDefault());
                        case ValueTypes.Float: return (double)Math.Pow(doubleA.GetValueOrDefault(), floatB.GetValueOrDefault());
                        case ValueTypes.Double: return (double)Math.Pow(doubleA.GetValueOrDefault(), doubleB.GetValueOrDefault());
                        default: return null;
                    }
                default: return null;
            }
        }



        /// <summary>
        /// Sum all elements from a specific array. The returned value can be different parameter of type informed array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sumValueType"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static object SumAllElementsFromArray(ValueTypes type, ValueTypes sumValueType, object array)
        {
            double sumValue = 0;
            switch (type)
            {
                case ValueTypes.SByte: foreach (sbyte value in ((sbyte[])array)) { sumValue += value; }; break;
                case ValueTypes.Byte: foreach (byte value in ((byte[])array)) { sumValue += value; }; break;
                case ValueTypes.UShort: foreach (ushort value in ((ushort[])array)) { sumValue += value; }; break;
                case ValueTypes.Short: foreach (short value in ((short[])array)) { sumValue += value; }; break;
                case ValueTypes.UInt: foreach (uint value in ((uint[])array)) { sumValue += value; }; break;
                case ValueTypes.Int: foreach (int value in ((int[])array)) { sumValue += value; }; break;
                case ValueTypes.ULong: foreach (ulong value in ((ulong[])array)) { sumValue += value; }; break;
                case ValueTypes.Long: foreach (long value in ((long[])array)) { sumValue += value; }; break;
                case ValueTypes.Float: foreach (float value in ((float[])array)) { sumValue += value; }; break;
                case ValueTypes.Double: foreach (double value in ((double[])array)) { sumValue += value; }; break;
            }
            switch (sumValueType)
            {
                case ValueTypes.SByte: return (sbyte)sumValue;
                case ValueTypes.Byte: return (byte)sumValue;
                case ValueTypes.UShort: return (ushort)sumValue;
                case ValueTypes.Short: return (short)sumValue;
                case ValueTypes.UInt: return (uint)sumValue;
                case ValueTypes.Int: return (int)sumValue;
                case ValueTypes.ULong: return (ulong)sumValue;
                case ValueTypes.Long: return (long)sumValue;
                case ValueTypes.Float: return (float)sumValue;
                case ValueTypes.Double: return (double)sumValue;
                default: return null;
            }
        }

        /// <summary>
        /// Sum all elements from a specific jagged array. The returned value can be different parameter of type informed array
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sumValueType"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object SumAllElementsFromJaggedArray(ValueTypes type, ValueTypes sumValueType, object jaggedArray)
        {
            double sumValue = 0;
            switch (type)
            {
                case ValueTypes.SByte: foreach (sbyte[] values in ((sbyte[][])jaggedArray)) { foreach (sbyte value in ((sbyte[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.Byte: foreach (byte[] values in ((byte[][])jaggedArray)) { foreach (byte value in ((byte[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.UShort: foreach (ushort[] values in ((ushort[][])jaggedArray)) { foreach (ushort value in ((ushort[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.Short: foreach (short[] values in ((short[][])jaggedArray)) { foreach (short value in ((short[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.UInt: foreach (uint[] values in ((uint[][])jaggedArray)) { foreach (uint value in ((uint[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.Int: foreach (int[] values in ((int[][])jaggedArray)) { foreach (int value in ((int[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.ULong: foreach (ulong[] values in ((ulong[][])jaggedArray)) { foreach (ulong value in ((ulong[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.Long: foreach (long[] values in ((long[][])jaggedArray)) { foreach (long value in ((long[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.Float: foreach (float[] values in ((float[][])jaggedArray)) { foreach (float value in ((float[])jaggedArray)) { sumValue += value; } } break;
                case ValueTypes.Double: foreach (double[] values in ((double[][])jaggedArray)) { foreach (double value in ((double[])jaggedArray)) { sumValue += value; } } break;
            }
            switch (sumValueType)
            {
                case ValueTypes.SByte: return (sbyte)sumValue;
                case ValueTypes.Byte: return (byte)sumValue;
                case ValueTypes.UShort: return (ushort)sumValue;
                case ValueTypes.Short: return (short)sumValue;
                case ValueTypes.UInt: return (uint)sumValue;
                case ValueTypes.Int: return (int)sumValue;
                case ValueTypes.ULong: return (ulong)sumValue;
                case ValueTypes.Long: return (long)sumValue;
                case ValueTypes.Float: return (float)sumValue;
                case ValueTypes.Double: return (double)sumValue;
                default: return null;
            }
        }

        /// <summary>
        /// Sum all column elements from a specific jagged array. The returned value will be a jagged array with 1 line
        /// </summary>
        /// <param name="type"></param>
        /// <param name="jaggedArray"></param>
        /// <returns></returns>
        public static object SumElementsFromJaggedArray(ValueTypes type, object jaggedArray)
        {
            ArrayLib.ValueTypes arrayLibType = (ArrayLib.ValueTypes)Enum.Parse(typeof(ArrayLib.ValueTypes), $"{type.GetHashCode()}");
            int[,] arraySizes = new int[,] { { ArrayLib.GetJaggedArrayRowSize(arrayLibType, jaggedArray), ArrayLib.GetJaggedArrayColumnSize(arrayLibType, jaggedArray) } };
            object sumJaggedArray = ArrayLib.NewJaggedArray(arrayLibType, 1, arraySizes[0, 1]);
            for (int i = 0; i < arraySizes[0, 1]; i++) { ArrayLib.InsertValueToJaggedArray(arrayLibType, ArrayAritmeticOperationsLib.SumAllElementsFromArray(type, type, ArrayLib.GetJaggedArrayColumn(arrayLibType, i, jaggedArray)), 0, i, ref sumJaggedArray); }
            return sumJaggedArray;
        }
        

        /// <summary>
        /// Dictionary to get all type relative with the used values from library ArrayLib 
        /// </summary>\
        public static Dictionary<Type, Tuple<StructTypes, ValueTypes>> Types
        {            
            get
            {               
                Dictionary<Type, Tuple<StructTypes, ValueTypes>> types = new Dictionary<Type, Tuple<StructTypes, ValueTypes>>
                {
                    {typeof(System.SByte),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.SByte)},
                    {typeof(System.Byte),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.Byte)},
                    {typeof(System.UInt16),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.UShort)},
                    {typeof(System.Int16),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.Short)},
                    {typeof(System.UInt32),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.UInt)},
                    {typeof(System.Int32),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.Int)},
                    {typeof(System.UInt64),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.ULong)},
                    {typeof(System.Int64),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.Long)},
                    {typeof(System.Single),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.Float)},
                    {typeof(System.Double),new Tuple<StructTypes, ValueTypes>(StructTypes.Value,ValueTypes.Double)},
                    {typeof(System.SByte[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.SByte)},
                    {typeof(System.Byte[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.Byte)},
                    {typeof(System.UInt16[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.UShort)},
                    {typeof(System.Int16[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.Short)},
                    {typeof(System.UInt32[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.UInt)},
                    {typeof(System.Int32[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.Int)},
                    {typeof(System.UInt64[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.ULong)},
                    {typeof(System.Int64[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.Long)},
                    {typeof(System.Single[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.Float)},
                    {typeof(System.Double[]),new Tuple<StructTypes, ValueTypes>(StructTypes.Array,ValueTypes.Double)},
                    {typeof(System.SByte[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.SByte)},
                    {typeof(System.Byte[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.Byte)},
                    {typeof(System.UInt16[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.UShort)},
                    {typeof(System.Int16[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.Short)},
                    {typeof(System.UInt32[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.UInt)},
                    {typeof(System.Int32[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.Int)},
                    {typeof(System.UInt64[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.ULong)},
                    {typeof(System.Int64[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.Long)},
                    {typeof(System.Single[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.Float)},
                    {typeof(System.Double[][]),new Tuple<StructTypes, ValueTypes>(StructTypes.JaggedArray,ValueTypes.Double)},
                };
                return types;
            }
        }

        /// <summary>
        /// <see cref="enum"/> with all value types used from library <see cref="ArrayAritmeticOperationsLib"/>
        /// </summary>
        public enum ValueTypes
        {
            SByte,
            Byte,
            UShort,
            Short,
            UInt,
            Int,
            ULong,
            Long,
            Float,
            Double,
        }
        /// <summary>
        /// <see cref="enum"/> with all struct types used from library <see cref="ArrayAritmeticOperationsLib"/>
        /// </summary>
        public enum StructTypes
        {
            Value,
            Array,
            JaggedArray,
        }
    }

    /// <summary>
    /// This class contains some logic operations, applyied to booleans arrays
    /// </summary>
    public static class ArrayLogicOperationsLib
    {
        public static bool[][] NOT(bool[][] jaggedArray)
        {
            if (jaggedArray.Length == 0) { return null; }
            int rowSize = jaggedArray.Length;
            int columnSize = jaggedArray[0].Length;
            bool[][] newJaggedArray = jaggedArray;

            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    newJaggedArray[i][j] = !newJaggedArray[i][j];
                }
            }
            return newJaggedArray;
        }
        public static bool[][] OR(bool[][] jaggedArrayA, bool[][] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[,] arraySizes = new int[,] { { jaggedArrayA.Length, jaggedArrayA[0].Length }, { jaggedArrayB.Length, jaggedArrayB[0].Length } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            bool[][] newJaggedArray = (bool[][])ArrayLib.NewJaggedArray(ArrayLib.ValueTypes.Bool, arraySizes[0, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    newJaggedArray[i][j] = jaggedArrayA[i][j] && jaggedArrayB[i][j];
                }
            }
            return newJaggedArray;
        }
        public static bool[][] AND(bool[][] jaggedArrayA, bool[][] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[,] arraySizes = new int[,] { { jaggedArrayA.Length, jaggedArrayA[0].Length }, { jaggedArrayB.Length, jaggedArrayB[0].Length } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            bool[][] newJaggedArray = (bool[][])ArrayLib.NewJaggedArray(ArrayLib.ValueTypes.Bool, arraySizes[0, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    newJaggedArray[i][j] = jaggedArrayA[i][j] || jaggedArrayB[i][j];
                }
            }
            return newJaggedArray;
        }
        public static bool[][] NOR(bool[][] jaggedArrayA, bool[][] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[,] arraySizes = new int[,] { { jaggedArrayA.Length, jaggedArrayA[0].Length }, { jaggedArrayB.Length, jaggedArrayB[0].Length } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            bool[][] newJaggedArray = (bool[][])ArrayLib.NewJaggedArray(ArrayLib.ValueTypes.Bool, arraySizes[0, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    newJaggedArray[i][j] = !(jaggedArrayA[i][j] || jaggedArrayB[i][j]);
                }
            }
            return newJaggedArray;
        }
        public static bool[][] NAND(bool[][] jaggedArrayA, bool[][] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[,] arraySizes = new int[,] { { jaggedArrayA.Length, jaggedArrayA[0].Length }, { jaggedArrayB.Length, jaggedArrayB[0].Length } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            bool[][] newJaggedArray = (bool[][])ArrayLib.NewJaggedArray(ArrayLib.ValueTypes.Bool, arraySizes[0, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    newJaggedArray[i][j] = !(jaggedArrayA[i][j] && jaggedArrayB[i][j]);
                }
            }
            return newJaggedArray;
        }
        public static bool[][] XOR(bool[][] jaggedArrayA, bool[][] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[,] arraySizes = new int[,] { { jaggedArrayA.Length, jaggedArrayA[0].Length }, { jaggedArrayB.Length, jaggedArrayB[0].Length } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            bool[][] newJaggedArray = (bool[][])ArrayLib.NewJaggedArray(ArrayLib.ValueTypes.Bool, arraySizes[0, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    newJaggedArray[i][j] = !jaggedArrayA[i][j] && jaggedArrayB[i][j] || jaggedArrayA[i][j] && !jaggedArrayB[i][j];
                }
            }
            return newJaggedArray;
        }
        public static bool[][] XNOR(bool[][] jaggedArrayA, bool[][] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[,] arraySizes = new int[,] { { jaggedArrayA.Length, jaggedArrayA[0].Length }, { jaggedArrayB.Length, jaggedArrayB[0].Length } };
            if (!(arraySizes[0, 0] == arraySizes[1, 0] && arraySizes[0, 1] == arraySizes[1, 1])) { return null; }
            bool[][] newJaggedArray = (bool[][])ArrayLib.NewJaggedArray(ArrayLib.ValueTypes.Bool, arraySizes[0, 0], arraySizes[0, 1]);

            for (int i = 0; i < arraySizes[0, 0]; i++)
            {
                for (int j = 0; j < arraySizes[0, 1]; j++)
                {
                    newJaggedArray[i][j] = !jaggedArrayA[i][j] && !jaggedArrayB[i][j] || jaggedArrayA[i][j] && jaggedArrayB[i][j];
                }
            }
            return newJaggedArray;
        }

        public static bool[] NOT(bool[] jaggedArray)
        {
            if (jaggedArray.Length == 0) { return null; }
            int rowSize = jaggedArray.Length;
            int columnSize = jaggedArray.Length;
            bool[] newJaggedArray = jaggedArray;

            for (int i = 0; i < rowSize; i++)
            {
                newJaggedArray[i] = !newJaggedArray[i];
            }
            return newJaggedArray;
        }
        public static bool[] OR(bool[] jaggedArrayA, bool[] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[] arraySizes = new int[] { jaggedArrayA.Length, jaggedArrayB.Length };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            bool[] newJaggedArray = new bool[arraySizes[0]];

            for (int i = 0; i < arraySizes[0]; i++)
            {

                newJaggedArray[i] = (jaggedArrayA[i] || jaggedArrayB[i]);

            }
            return newJaggedArray;
        }
        public static bool[] AND(bool[] jaggedArrayA, bool[] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[] arraySizes = new int[] { jaggedArrayA.Length, jaggedArrayB.Length };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            bool[] newJaggedArray = new bool[arraySizes[0]];

            for (int i = 0; i < arraySizes[0]; i++)
            {

                newJaggedArray[i] = (jaggedArrayA[i] && jaggedArrayB[i]);

            }
            return newJaggedArray;
        }
        public static bool[] NOR(bool[] jaggedArrayA, bool[] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[] arraySizes = new int[] { jaggedArrayA.Length, jaggedArrayB.Length };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            bool[] newJaggedArray = new bool[arraySizes[0]];

            for (int i = 0; i < arraySizes[0]; i++)
            {

                newJaggedArray[i] = !(jaggedArrayA[i] || jaggedArrayB[i]);

            }
            return newJaggedArray;
        }
        public static bool[] NAND(bool[] jaggedArrayA, bool[] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[] arraySizes = new int[] { jaggedArrayA.Length, jaggedArrayB.Length };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            bool[] newJaggedArray = new bool[arraySizes[0]];

            for (int i = 0; i < arraySizes[0]; i++)
            {

                newJaggedArray[i] = !(jaggedArrayA[i] && jaggedArrayB[i]);

            }
            return newJaggedArray;
        }
        public static bool[] XOR(bool[] jaggedArrayA, bool[] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[] arraySizes = new int[] { jaggedArrayA.Length, jaggedArrayB.Length };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            bool[] newJaggedArray = new bool[arraySizes[0]];

            for (int i = 0; i < arraySizes[0]; i++)
            {

                newJaggedArray[i] = !jaggedArrayA[i] && jaggedArrayB[i] || jaggedArrayA[i] && !jaggedArrayB[i];

            }
            return newJaggedArray;
        }
        public static bool[] XNOR(bool[] jaggedArrayA, bool[] jaggedArrayB)
        {
            if (jaggedArrayA.Length == 0 || jaggedArrayB.Length == 0) { return null; }
            int[] arraySizes = new int[] { jaggedArrayA.Length, jaggedArrayB.Length };
            if (!(arraySizes[0] == arraySizes[1])) { return null; }
            bool[] newJaggedArray = new bool[arraySizes[0]];

            for (int i = 0; i < arraySizes[0]; i++)
            {

                newJaggedArray[i] = !jaggedArrayA[i] && !jaggedArrayB[i] || jaggedArrayA[i] && jaggedArrayB[i];

            }
            return newJaggedArray;
        }
    }
}
