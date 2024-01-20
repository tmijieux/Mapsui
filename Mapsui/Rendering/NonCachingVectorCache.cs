﻿using System;

namespace Mapsui.Rendering;

public sealed class NonCachingVectorCache(ISymbolCache symbolCache) : IVectorCache
{
    public CacheTracker<T> GetOrCreatePaint<T, TParam>(TParam param, Func<TParam, T> toPaint)
    {
        return new CacheTracker<T>(toPaint(param));
    }

    public CacheTracker<T> GetOrCreatePaint<T, TParam>(TParam param, Func<TParam, ISymbolCache, T> toPaint)
    {
        return new CacheTracker<T>(toPaint(param, symbolCache));
    }

    public CacheTracker<T> GetOrCreatePath<T, TParam>(TParam param, Func<TParam, T> toSkRect)
    {
        return new CacheTracker<T>(toSkRect(param));
    }

    public void Dispose()
    {
    }
}
