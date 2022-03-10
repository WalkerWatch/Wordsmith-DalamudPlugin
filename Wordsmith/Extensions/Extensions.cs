﻿
namespace Wordsmith.Extensions;

public static class Extensions
{
    /// <summary>
    /// Move an item inside of a List of T.
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    /// <param name="list">The extended list</param>
    /// <param name="obj">The object to move</param>
    /// <param name="index">The index to move to. Offset will be calculated already. If out of range, obj will be moved to first or last slot.</param>
    public static void Move<T>(this List<T> list, T obj, int index)
    {
        // Get the current index of the object.
        int idx = list.IndexOf(obj);

        // If the current index is lower than the index we're moving to we actually
        // lower the new index by 1 because the indices will shift once we remove
        // the item from it's current place in the list.
        if (idx < index)
            --index;

        // If the index where we are moving the object to is the same as where it
        // is already located then simply return.
        else if (idx == index)
            return;

        // Remove the object
        list.Remove(obj);

        // As a failsafe, if the index is passed the end of the list, move the
        // object to the end of the list.
        if (index >= list.Count)
            list.Add(obj);

        // As a second failsafe, if the index is below zero, move the object to
        // the front of the list.
        else if (index < 0)
            list.Insert(0, obj);

        else
            // Insert it at the new location.
            list.Insert(index, obj);
    }

    /// <summary>
    /// Capitalizes the first letter in a string.
    /// </summary>
    /// <param name="s">The string to capitalize the first letter of.</param>
    /// <returns></returns>
    public static string CaplitalizeFirst(this string s)
    {
        // If the length is one, just change the char and send it back.
        if (s.Length == 1)
            return char.ToUpper(s[0]).ToString();

        // If the length is greater than 1, capitalize the first char and
        // get the remaining substring to lower.
        else if (s.Length > 1)
            return char.ToUpper(s[0]).ToString() + s.Substring(1).ToLower();

        // If we reach this return, the string is empty, return as-is.
        return s;
    }

    public static string SpaceByCaps(this string s)
    {
        // If there aren't at least two characters then return.
        if (s.Length < 2)
            return s;

        string result = s[0].ToString();

        string caps = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        // Iterate through each character not in the first index
        for (int i =1; i< s.Length; ++i)// char c in s[1..^0])
        {
            // If the character is a capital letter and the letter before
            // it is not then add a space.
            if (caps.Contains(s[i]) && !caps.Contains(s[i-1]))
                result += " ";

            // Add the character to the result.
            result += s[i];
        }

        return result;
    }

    /// <summary>
    /// Removes all double spaces from a string.
    /// </summary>
    /// <param name="s">The string to remove double spaces from.</param>
    /// <returns></returns>
    public static string FixSpacing(this string s)
    {
        // Start by initially running the replace command.
        do
        {
            // Replace double spaces.
            s = s.Replace("  ", " ");

            // Loop because 3 spaces together will only get knocked down
            // to 2 spaces and it won't check again so we need to. With
            // each pass, any area with more than one space will become
            // less spaced until only one remains.
        } while (s.Contains("  "));

        // Return the correctly spaced string.
        return s;
    }

    public static string FixSpacing(this string s, ref int cursorPos)
    {
        do
        {
            // Get the position of the first double space.
            int idx = s.IndexOf("  ");

            // If the index is greater than -1;
            if (idx > -1)
            {
                // If the index is 0 just remove the space from the front of the line.
                if (idx == 0)
                    s = s[1..^0];

                // Remove the space from inside the string.
                else
                    s = s[0..idx] + s[(idx + 1)..^0];

                // If the removed space is at a lower index than the cursor
                // move the cursor back a space to account for the position change.
                if (idx <= cursorPos)
                    cursorPos -= 1;
            }
        } while (s.Contains("  "));

        return s;
    }

    /// <summary>
    /// Returns the index of an item in an array.
    /// </summary>
    /// <typeparam name="T">Generic Type</typeparam>
    /// <param name="array">The array to find the object in.</param>
    /// <param name="obj">The object to locate within the array.</param>
    /// <returns></returns>
    public static int IndexOf<T>(this T[] array, T obj)
    {
        // Iterate and compare each item and return the index if
        // a match is found.
        for (int i = 0; i < array.Length; ++i)
            if (array[i]?.Equals(obj) ?? false)
                return i;

        // If no match is found, return -1 to signal that it isn't in
        // the array.
        return -1;
    }
}
