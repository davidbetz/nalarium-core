# Nalarium (.net core) - Extended BCL for .net core

**Copyright (c) 2005-2017 David Betz**

[![Build Status](https://travis-ci.org/davidbetz/nalarium.svg?branch=master)](https://travis-ci.org/davidbetz/nalarium)
[![NuGet Version and Downloads count](https://buildstats.info/nuget/nalarium)](https://www.nuget.org/packages/nalarium)

You do stuff enough you'll want to stop repeating yourself. You'll want to be DRY. This is my desert.

I add my regularly and common stuff to Nalarium. It's my .NET crutch as much as [understore.js](http://underscorejs.org/) is my crutch in ES5.

### Totally out-of-context Nalarium.dll sample:

    var path = @"E:\Drive\Documents\Content\NetFX\NetFXContent\2009\06";
    var url = Url.FromPath(path);
    var ultima = Url.GetPart(context, Position.Penultima);
    //+ ultimate == 2009

Unit tests act as practical documentation. For example:


    public void ParseBoolean()
    {
      Assert.True(Nalarium.Parser.ParseBoolean(1));
      Assert.True(Nalarium.Parser.ParseBoolean("1"));
      Assert.True(Nalarium.Parser.ParseBoolean("1.0"));
      Assert.True(Nalarium.Parser.ParseBoolean("true"));
      Assert.True(Nalarium.Parser.ParseBoolean("True"));
      Assert.True(Nalarium.Parser.ParseBoolean("active"));
      Assert.True(Nalarium.Parser.ParseBoolean("on"));
      Assert.False(Nalarium.Parser.ParseBoolean("12"));
      Assert.False(Nalarium.Parser.ParseBoolean(12));
    }

  and

    public void Decompose()
    {
      var results = Nalarium.Binary.Decompose(12345);

      var expected = new List<long>(new long[] { 1, 8, 16, 32, 4096, 8192 });

      Assert.Equal(expected, results);
    }