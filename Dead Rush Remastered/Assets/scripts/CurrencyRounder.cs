using System;

public static class CurrencyRounder
{
    public static string Round(int v)
    {
        if (v < 0)
        {
            throw new ArgumentException("value < 0");
        }
        float cof = 1000.0f;
        if (v > 999)
        {
            return string.Format("{0:#.000}", v / cof) + "K";
        }

        if (v > 999999)
        {
            cof = 1000000.0f;
            return string.Format("{0:#.000}", v / cof) + "M";
        }

        if (v > 999999999)
        {
            cof = 1000000000.0f;
            return string.Format("{0:#.000}", v / cof) + "B";
        }



        return $"{v}";

    }

    public static string Round(long v)
    {
        if (v < 0)
        {
            throw new ArgumentException("value < 0");
        }
        float cof = 1000.0f;
        if (v > 999)
        {
            return string.Format("{0:#.000}", v / cof) + "K";
        }

        if (v > 999999)
        {
            cof = 1000000.0f;
            return string.Format("{0:#.000}", v / cof) + "M";
        }

        if (v > 999999999)
        {
            cof = 1000000000.0f;
            return string.Format("{0:#.000}", v / cof) + "B";
        }

        if (v > 999999999999)
        {
            cof = 1000000000000.0f;
            return string.Format("{0:#.000}", v / cof) + "T";
        }



        return $"{v}";
    }

    public static string Round(double v)
    {
        if (v < 0)
        {
            throw new ArgumentException("value < 0");
        }
        float cof = 1000.0f;
        if (v > 999)
        {
            return string.Format("{0:#.000}", v / cof) + "K";
        }

        if (v > 999999)
        {
            cof = 1000000.0f;
            return string.Format("{0:#.000}", v / cof) + "M";
        }

        if (v > 999999999)
        {
            cof = 1000000000.0f;
            return string.Format("{0:#.000}", v / cof) + "B";
        }

        if (v > 999999999999)
        {
            cof = 1000000000000.0f;
            return string.Format("{0:#.000}", v / cof) + "T";
        }



        return $"{v}";
    }

    public static string Round(float v)
    {
        if (v < 0)
        {
            throw new ArgumentException("value < 0");
        }
        float cof = 1000.0f;
        if (v > 999)
        {
            return string.Format("{0:#.000}", v / cof) + "K";
        }

        if (v > 999999)
        {
            cof = 1000000.0f;
            return string.Format("{0:#.000}", v / cof) + "M";
        }

        if (v > 999999999)
        {
            cof = 1000000000.0f;
            return string.Format("{0:#.000}", v / cof) + "B";
        }

        if (v > 999999999999)
        {
            cof = 1000000000000.0f;
            return string.Format("{0:#.000}", v / cof) + "T$";
        }



        return $"{v}";
    }
}
