# 序列化模块使用方法

## 定义可序列化类型

- 给需要Nino序列化/反序列化的类或结构体，打上```[NinoSerialize()]```标签，如果**需要自动收集全部字段和属性，则该标签内部加入个true参数**，如```[NinoSerialize(true)]```
- 如果**没有自动收集全部字段和属性**，则需要给想序列化/反序列化的字段或属性，打上```[NinoMember()]```标签，标签内部需要传入一个数字参数，即序列化和反序列化时该成员的位置，如```[NinoMember(1)]```

代码示范：

```csharp
[NinoSerialize(true)]
public partial class IncludeAllClass
{
  public int a;
  public long b;
  public float c;
  public double d;

  public override string ToString()
  {
    return $"{a}, {b}, {c}, {d}";
  }
}

[NinoSerialize()]
public partial class NotIncludeAllClass
{
  [NinoMember(1)]
  public int a;
  [NinoMember(2)]
  public long b;
  [NinoMember(3)]
  public float c;
  [NinoMember(4)]
  public double d;

  public override string ToString()
  {
    return $"{a}, {b}, {c}, {d}";
  }
}
```

> 建议每个需要Nino序列化/反序列化的类和结构体用partial定义，这样可以生成代码
>
> 推荐下面的写法，给每个字段或属性单独打```NinoMember```标签，这样性能最好，体积最小
>
> **不推荐上面的类型的写法（自动收集），因为会导致生成出来的体积较大，序列化反序列化速度慢，并且无法生成代码优化性能**



## 支持类型

支持序列化的成员类型（底层自带支持）：

- byte, sbyte, short, ushort, int, uint, long, ulong, double, float, decimal, char, string, bool, enum
- List<上述类型>，上述类型[]
- List<可Nino序列化类型>，可Nino序列化类型[]
- List<注册委托类型>，注册委托类型[]
- Dictionary<Nino支持类型,Nino支持类型>
- Dictionary<注册委托类型,注册委托类型>
- 可Nino序列化类型（代表可以嵌套）

不支持序列化的成员类型（可以通过注册自定义委托实现）：

- 任何非上述类型（Nullable, DateTime，Vector3等）

**针对某个类型注册自定义序列化委托后，记得注册该类型的自定义反序列化委托，不然会导致反序列化出错**



## 注册序列化委托

给指定类型注册该委托后，全局序列化的时候遇到该类型会直接使用委托方法写入二进制数据

需要注意的是，不支持注册底层自带支持的类型的委托

使用方法：

```csharp
Serializer.AddCustomImporter<T>((val, writer) =>
                                                  {
                                                    //TODO use writer to write
                                                  });
```

T是需要注册的类型的泛型参数，val是T的实例，writer是用来写二进制的工具

示例：

```csharp
Serializer.AddCustomImporter<UnityEngine.Vector3>((val, writer) =>
                                                  {
                                                    //write 3 float
                                                    writer.Write(val.x);
                                                    writer.Write(val.y);
                                                    writer.Write(val.z);
                                                  });
```

这里我们写了个Vector3，将其x,y,z以float的方式写入



## 注册反序列化委托

给指定类型注册该委托后，全局翻序列化的时候遇到该类型会直接使用委托方法读取二进制数据并转为对象

需要注意的是，不支持注册底层自带支持的类型的委托

使用方法：

```csharp
Deserializer.AddCustomExporter<T>(reader =>
                                  //TODO return T instance
                                 );
```

T是需要注册的类型的泛型参数，reader是用来读二进制的工具

示例：

```csharp
Deserializer.AddCustomExporter<UnityEngine.Vector3>(reader =>
                                                    new UnityEngine.Vector3(reader.ReadFloat(), reader.ReadFloat(), reader.ReadFloat()));
```

这里我们读了3个float作为xyz（因为写入的时候写3个float，xyz），创建了Vector3并返回



## 代码生成

- Unity下直接在菜单栏点击```Nino/Generator/Serialization Code```即可，代码会生成到```Assets/Nino/Generated```，也可以打开```Assets/Nino/Editor/SerializationHelper.cs```并修改内部的```ExportPath```参数
- 非Unity下调用```CodeGenerator.GenerateSerializationCodeForAllTypePossible```接口即可

## 序列化

```csharp
byte[] byteArr = Nino.Serialization.Serializer.Serialize<ObjClass>(obj);
```

传入需要序列化的类型作为泛型参数，以及该类型的实例，会返回二进制数组

## 反序列化

```csharp
var obj = Nino.Serialization.Deserializer.Deserialize<ObjClass>(byteArr);
```

传入需要反序列化的类型作为泛型参数，以及序列化结果的二进制数组，会返回反序列化出的对象实例


