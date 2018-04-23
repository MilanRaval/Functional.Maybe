using System;
using System.Diagnostics.Contracts;

namespace Functional.Maybe
{
	/// <summary>
	/// Applying side effects into the Maybe call chain
	/// </summary>
	public static class MaybeSideEffects
	{
		/// <summary>
		/// Calls <paramref name="fn"/> if <paramref name="m"/> has value, otherwise does nothing
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="m"></param>
		/// <param name="fn"></param>
		/// <returns></returns>
		public static Maybe<T> Do<T>(this Maybe<T> m, Action<T> fn)
		{
			if (m.IsSomething())
				fn(m.Value);
			return m;
		}

        /// <summary>
        /// Calls <paramref name="fn"/> if <paramref name="m"/> has value and predicate <paramref name="predicate"/> is true, otherwise does nothing
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static void DoWhenTrue<T>(this Maybe<T> m, Func<T, bool> predicate, Action<T> fn)
        {
            Contract.Requires(predicate != null);
            if (m.IsSomething())
            {
                if (predicate(m.Value))
                {
                    fn(m.Value);
                }
            }
        }


        /// <summary>
        /// Calls <paramref name="fn"/> if <paramref name="m"/> has value, otherwise calls <paramref name="else"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="m"></param>
        /// <param name="fn"></param>
        /// <param name="else"></param>
        /// <returns></returns>
        public static Maybe<T> Match<T>(this Maybe<T> m, Action<T> fn, Action @else)
		{
			if (m.IsSomething())
				fn(m.Value);
			else
				@else();
			return m;
		}
	}
}