### EntitySys简单介绍
---

###### SimplestEntitySys

其定义如下所示
```CSharp
public abstract class SimplestEntitySys<T_entity, T_container> : MonoBehaviour
    where T_entity : IEntity
    where T_container : IEntityContainer<T_entity>, IEnumerable<T_entity>, new()
```
`T_entity` : 实体系统的实体类型
`T_container` : 实体容器，实体系统使用该容器存储实体

使用方法：继承自`SimplestEntitySys`
- 调用对应的`Instance.NewEntity<T>()`创建实体
- 手动new出实体后`Instance.NewEntity<T>(e)`注册实体

生命周期：
`T_entity.new()` --> 无论是手动new还是EntitySys自动new，构造函数总是第一个调用
`SimplestEntitySys.AllocateID()` --> EntitySys会首先为new出来的对象分配一个ID，每个active对象的ID总是唯一的
`SimplestEntitySys.MarkToRegistered()` --> 将该对象标记为Need Register
...
`Wait to Nearst SimplestEntitySys.Update()` --> 等待到下一次Unity的Update事件
`AddToEntityContainer(e)` --> 将对象放入容器
- *`e.OnInstantiate()`* -->  如果对象的容器是`CSObjectPool`,那么在此时还会调用对象的`OnInstantiate`方法
  
`OnRegisterEntity(e)` --> 调用`OnRegisterEntity`方法，该方法为抽象方法，由各个实体系统自行实现
`UpdateEntity(e)` --> 实体系统的Update函数
`RemoveEntity(e)` --> 将被标记为删除的实体删除

---

###### UObjEntitySys

其定义如下所示
```CSharp
public abstract class UObjEntitySys<T_entity, T_entityContainer, T_UObject, T_UObjectContainer> : SimplestEntitySys<T_entity, T_entityContainer>
    where T_entity : IUEntity<T_UObject>
    where T_entityContainer : IEntityContainer<T_entity>, IEnumerable<T_entity>, new()
    where T_UObjectContainer : IEntityContainer<T_UObject>, IEnumerable<T_UObject>, new()
```
`T_entity` : 实体系统的实体类型
`T_container` : 实体容器，实体系统使用该容器存储实体
`T_UObject` : Unity对象类型
`T_UObjectContainer` : Unity对象的容器

使用方法：继承自`UObjEntitySys`
- 调用对应的`Instance.NewEntity<T>()`创建实体
- 手动new出实体后`Instance.NewEntity<T>(e)`注册实体

生命周期：
`T_entity.new()` --> 无论是手动new还是EntitySys自动new，构造函数总是第一个调用
`UObjEntitySys.AllocateID()` --> EntitySys会首先为new出来的对象分配一个ID，每个active对象的ID总是唯一的
`UObjEntitySys.MarkToRegistered()` --> 将该对象标记为Need Register
...
`Wait to Nearst UObjEntitySys.Update()` --> 等待到下一次Unity的Update事件
`AddToEntityContainer(e)` --> 将对象放入容器
- *`e.OnInstantiate()`* -->  如果对象的容器是`CSObjectPool`,那么在此时还会调用对象的`OnInstantiate`方法

`UObjEntitySys.GetContainer()` --> 每种UObject在各自的实体系统里都有唯一的ID，使用该唯一的ID获取一个实体容器
`UObjEntitySys.GetUObjectFromContainer()` --> 从该容器中获取一个UObject
`OnInstantiateUObject(e)` --> 调用`OnInstantiateUObject`方法，该方法为抽象方法，由各个实体系统自行实现
`UpdateEntity(e)` --> 实体系统的Update函数
`RemoveEntity(e)` --> 将被标记为删除的实体删除