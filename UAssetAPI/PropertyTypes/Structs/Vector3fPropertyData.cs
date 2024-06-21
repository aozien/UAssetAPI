﻿using System;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.UnrealTypes;

namespace UAssetAPI.PropertyTypes.Structs;

/// <summary>
/// A vector in 3-D space composed of components (X, Y, Z) with floating point precision.
/// </summary>
public struct FVector3f : ICloneable
{
    public float X;
    public float Y;
    public float Z;

    public FVector3f(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public FVector3f(AssetBinaryReader reader)
    {
        X = reader.ReadSingle();
        Y = reader.ReadSingle();
        Z = reader.ReadSingle();
    }

    public int Write(AssetBinaryWriter writer)
    {
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Z);
        return sizeof(float) * 3;
    }

    public object Clone() => new FVector3f(X, Y, Z);
}


public class Vector3fPropertyData : PropertyData<FVector3f>
{
    public Vector3fPropertyData(FName name) : base(name) { }

    public Vector3fPropertyData() { }

    private static readonly FString CurrentPropertyType = new FString("Vector3f");
    public override bool HasCustomStructSerialization => true;
    public override FString PropertyType => CurrentPropertyType;

    public override void Read(AssetBinaryReader reader, bool includeHeader, long leng1, long leng2 = 0, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            PropertyGuid = reader.ReadPropertyGuid();
        }

        Value = new FVector3f(reader);
    }

    public override int Write(AssetBinaryWriter writer, bool includeHeader, PropertySerializationContext serializationContext = PropertySerializationContext.Normal)
    {
        if (includeHeader)
        {
            writer.WritePropertyGuid(PropertyGuid);
        }

        return Value.Write(writer);
    }

    public override void FromString(string[] d, UAsset asset)
    {
        float.TryParse(d[0], out float X);
        float.TryParse(d[1], out float Y);
        float.TryParse(d[2], out float Z);
        Value = new FVector3f(X, Y, Z);
    }

    public override string ToString()
    {
        return "(" + Value.X + ", " + Value.Y + ", " + Value.Z + ")";
    }
}