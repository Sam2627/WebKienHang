namespace WebVanChuyen.Api.Logic
{
    public static class SearchRouteAdd
    {
        public static Task MoveItemAtIndexToFront<T>(this List<T> list, int index)
        {
            // Get item at index form List
            T item = list[index];
            // Move items of the left of index to right by 1
            // Add item at the front
            for (int i = index; i > 0; i--) list[i] = list[i - 1];
            // Add item to front
            list[0] = item;

            return Task.CompletedTask;
        }

        // Push-up items behind remove index
        public static Task ArrangeItemsAfterRemoveIndex<T>(this List<T> list, int index)
        {
            var maxIndex = list.Count;

            // Set i start = item behind remove item -- index + 1
            // Set condition is number of items in list
            // Move items behind to left by one
            for (int i = index + 1; i < maxIndex - 1; i++) list[i] = list[i + 1];

            // Remove last duplicate items
            list.RemoveAt(maxIndex - 1);

            return Task.CompletedTask;
        }
    }
}
