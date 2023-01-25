using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpellMaker : MonoBehaviour, IResetOnRestart
{
    [SerializeField] private ElementBar _elementBar;

    private List<MagicElementCell> _selectedCells = new List<MagicElementCell>();

    public event UnityAction<List<ElementType>> CombinationUpdated;

    private void OnEnable()
    {
        _elementBar.CellAdded += OnCellAdded;
    }

    private void OnDisable()
    {
        _elementBar.CellAdded -= OnCellAdded;
    }

    public void DeactivateCurrentCombination()
    {
        int count = _selectedCells.Count;
        
        for (int i = 0; i < count; i++)
        {
            var cell = _selectedCells[0];
            cell.ToggleSelection();
            cell.ChangeElement();
        }
    }

    public void UnselectCurrentCombination()
    {
        for (int i = _selectedCells.Count - 1; i >= 0; i--)
            _selectedCells[i].ToggleSelection();
    }

    public void Reset()
    {
        UnselectCurrentCombination();
    }

    private void OnCellAdded(MagicElementCell cell)
    {
        cell.Toggled += OnCellSelectionChanged;
    }

    private void OnCellSelectionChanged(MagicElementCell cell, bool isSelected)
    {
        if (isSelected)
            _selectedCells.Add(cell);
        else
            _selectedCells.Remove(cell);

        List<ElementType> selectedElements = new List<ElementType>();

        foreach(var elementCell in _selectedCells)
            selectedElements.Add(elementCell.CurrentElement);

        CombinationUpdated?.Invoke(selectedElements);
    }
}
