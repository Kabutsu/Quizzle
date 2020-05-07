﻿using System.Collections.Generic;

public class UserComparer : IComparer<User>
{
    public int Compare(User x, User y)
    {
        if (x == null)
        {
            if (y == null)
            {
                // If x is null and y is null, they're
                // equal.
                return 0;
            }
            else
            {
                // If x is null and y is not null, y
                // is greater.
                return -1;
            }
        }
        else
        {
            // If x is not null...
            if (y == null)
            {
                // ...and y is null, x is greater.
                return 1;
            }
            else
            {
                // ...and y is not null, compare the
                // sizes of the responses.
                int retval = x.Score.CompareTo(y.Score);
                if (retval != 0)
                {
                    // If the responses are not of equal length,
                    // the larger response is greater.
                    return retval;
                }
                else
                {
                    // Otherwise the answers are equal.
                    return 0;
                }
            }
        }
    }
}
