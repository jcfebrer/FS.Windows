using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FSFormControls
{
    public class DBGridViewRowCollection : IEnumerable<DBGridViewRow>
    {
        private DataGridView _ownerDataGridView;
        private List<DBGridViewRow> _internalRows;

        public DBGridViewRowCollection(DataGridView owner)
        {
            _ownerDataGridView = owner;
            _internalRows = new List<DBGridViewRow>();
            Console.WriteLine("DBGridViewRowCollection creada con un DataGridView owner.");
        }

        // Método para añadir una nueva DBGridViewRow a la colección interna
        public void Add(DBGridViewRow row)
        {
            _internalRows.Add(row);
        }

        // Método para añadir filas desde un DataGridView existente
        public void AddRowsFromDataGridView(DataGridView sourceGrid)
        {
            if (sourceGrid == null) return;

            foreach (DataGridViewRow row in sourceGrid.Rows)
            {
                // Clona la fila para no depender de la instancia original del DataGridView
                DBGridViewRow newRow = new DBGridViewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    // Asegúrate de que las celdas se clonen o sus valores se copien correctamente
                    // Esto es crucial para que la fila clonada tenga las mismas celdas y valores
                    if (newRow.Cells.Count <= i)
                    {
                        newRow.Cells.Add(new DataGridViewTextBoxCell());
                    }
                    newRow.Cells[i].Value = row.Cells[i].Value;
                }
                _internalRows.Add(newRow); // Añade a tu colección interna
            }
        }

        // Aquí podrías añadir otros métodos para acceder o manipular tus filas internas
        public DataGridViewRow GetRow(int index)
        {
            return _internalRows[index];
        }

        public IEnumerator<DBGridViewRow> GetEnumerator()
        {
            return _internalRows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _internalRows.Count; }
        }

        public new DBGridViewRow this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");
                }

                return (DBGridViewRow)_internalRows[index];
            }
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index out of range");
            }
            _internalRows.RemoveAt(index);
        }

        public DBGridViewFilterCollection ColumnFilters { get; set; }
    }
}