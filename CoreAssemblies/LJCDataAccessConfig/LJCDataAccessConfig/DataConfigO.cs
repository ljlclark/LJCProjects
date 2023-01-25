// Copyright(c) Lester J. Clark and Contributors.
// Licensed under the MIT License.
// DataConfigO.cs
using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace LJCDataAccessConfig
{
  /// <summary>NoGen</summary>
  public partial class DataConfig
  {
    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public string B(string a)
    {
      string b = null; string c = F(a);
      if (c != null)
      {
        byte[] d = H(); byte[] e = J(); byte[] f = C(c);
        byte[] g = K(f, d, e);
        byte[] h = new byte[d.Length + e.Length + g.Length];
        d.CopyTo(h, 0); e.CopyTo(h, d.Length);
        g.CopyTo(h, d.Length + e.Length); b = C(h);
      }
      return b;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public static byte[] C(string a)
    {
      string b = a.Trim(); byte[] c = new byte[b.Length];
      for (int index = 0; index < b.Length; index++)
      {
        c[index] = Convert.ToByte(b[index]);
      }
      return c;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public static string C(byte[] a)
    {
      byte[] b; b = D(a);
      return Encoding.ASCII.GetString(b);
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public static byte[] D(byte[] a)
    {
      char[] b; byte[] c; long d;
      d = (long)((4.0d / 3.0d) * a.Length);
      if (d % 4 != 0)
      {
        d += 4 - d % 4;
      }
      b = new char[d];
      Convert.ToBase64CharArray(a, 0, a.Length, b, 0);
      c = new byte[b.Length];
      for (int index = 0; index < c.Length; index++)
      {
        c[index] = (byte)b[index];
      }
      return c;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public string E(string a)
    {
      string b;
      byte[] c = Convert.FromBase64String(a);
      byte[] d = new byte[32]; byte[] e = new byte[16];
      Array.Copy(c, 0, d, 0, d.Length);
      Array.Copy(c, d.Length, e, 0, e.Length);
      int f = c.Length - (d.Length + e.Length);
      byte[] g = new byte[f];
      Array.Copy(c, (d.Length + e.Length), g, 0, g.Length);
      b = N(g, d, e); return b;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public static string F(string a)
    {
      string retVal = null;
      if (a != null && a.Trim().Length > 0)
      {
        retVal = a.Trim();
      }
      return retVal;
    }

    /// <summary>NoGen</summary>
    public byte[] H()
    {
      G.GenerateKey(); return G.Key;
    }

    /// <summary>NoGen</summary>
    public byte[] J()
    {
      G.GenerateIV(); return G.IV;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    /// <param name="b">b</param>
    /// <param name="c">c</param>
    public byte[] K(byte[] a, byte[] b, byte[] c)
    {
      CryptoStream d; Stream e; Stream f;
      BinaryWriter g; int h; byte[] i; byte[] j;
      G.Key = b; G.IV = c; e = L(a);
      d = new CryptoStream(e, G.CreateEncryptor()
        , CryptoStreamMode.Read);
      f = new MemoryStream(); g = new BinaryWriter(f);
      i = new byte[1024];
      do
      {
        h = d.Read(i, 0, 1024); g.Write(i, 0, h);
      } while (h > 0);
      g.Flush(); j = M(f);
      g.Close(); f.Close(); e.Close(); d.Clear();
      d.Close(); return j;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public static Stream L(byte[] a)
    {
      MemoryStream b = new MemoryStream(a, 0, a.Length)
      {
        Position = 0
      };
      return b;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    public static byte[] M(Stream a)
    {
      byte[] b = new byte[a.Length];
      a.Position = 0;
      for (int c = 0; c < a.Length; c++)
      {
        b[c] = Convert.ToByte(a.ReadByte());
      }
      a.Position = 0; return b;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    /// <param name="b">b</param>
    /// <param name="c">c</param>
    public string N(byte[] a, byte[] b, byte[] c)
    {
      byte[] d; string e;
      d = O(a, b, c);
      e = Encoding.ASCII.GetString(d); return e;
    }

    /// <summary>NoGen</summary>
    /// <param name="a">a</param>
    /// <param name="b">b</param>
    /// <param name="c">c</param>
    public byte[] O(byte[] a, byte[] b, byte[] c)
    {
      CryptoStream d; Stream e; Stream f;
      int g; byte[] h; byte[] i;
      G.Key = b; G.IV = c;
      f = new MemoryStream();
      d = new CryptoStream(f, G.CreateDecryptor()
        , CryptoStreamMode.Write);
      e = L(a); h = new byte[1024];
      do
      {
        g = e.Read(h, 0, 1024); d.Write(h, 0, g);
      } while (g > 0);
      d.FlushFinalBlock(); i = M(f);
      f.Close(); e.Close(); d.Clear();
      d.Close(); return i;
    }

    private readonly SymmetricAlgorithm G;
  }
}
