using UnityEngine;
using System;
using System.Collections.Generic;

public class Combo : IComparable<Combo> {
    public string[] buttons;

    public int CompareTo(Combo other) {
        return buttons.Length - other.buttons.Length;
    }

    public bool Match(string[] queue) {
        int start = queue.Length - buttons.Length;
        if (start < 0) return false;

        for (int i = 0; i < buttons.Length; ++i)
            if (string.Compare(buttons[i], queue[start + i], true) != 0) return false;

        return true;
    }

    public static Combo Parse(string str) {
        return new Combo {
            buttons = str.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
        };
    }

    public override string ToString() {
        return string.Join(" ", buttons);
    }
}

public class ComboList : MonoBehaviour {

    public string[] combos;

    private Combo[] sortedCombos;

    private void Awake() {
        sortedCombos = Array.ConvertAll(combos, combo => Combo.Parse(combo));
    }
}
