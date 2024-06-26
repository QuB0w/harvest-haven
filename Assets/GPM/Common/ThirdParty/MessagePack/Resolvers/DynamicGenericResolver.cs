﻿using System.Linq;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections;

namespace Gpm.Common.ThirdParty.MessagePack.Resolvers
{
    using Formatters;
    using Internal;
    public sealed class DynamicGenericResolver : IFormatterResolver
    {
        public static readonly IFormatterResolver Instance = new DynamicGenericResolver();

        DynamicGenericResolver()
        {

        }

        public IMessagePackFormatter<T> GetFormatter<T>()
        {
            return FormatterCache<T>.formatter;
        }

        static class FormatterCache<T>
        {
            public static readonly IMessagePackFormatter<T> formatter;

            static FormatterCache()
            {
                formatter = (IMessagePackFormatter<T>)DynamicGenericResolverGetFormatterHelper.GetFormatter(typeof(T));
            }
        }
    }
}

namespace Gpm.Common.ThirdParty.MessagePack.Internal
{
    using Formatters;
    internal static class DynamicGenericResolverGetFormatterHelper
    {
        static readonly Dictionary<Type, Type> formatterMap = new Dictionary<Type, Type>()
        {
              {typeof(List<>), typeof(ListFormatter<>)},
              {typeof(LinkedList<>), typeof(LinkedListFormatter<>)},
              {typeof(Queue<>), typeof(QeueueFormatter<>)},
              {typeof(Stack<>), typeof(StackFormatter<>)},
              {typeof(HashSet<>), typeof(HashSetFormatter<>)},
              {typeof(ReadOnlyCollection<>), typeof(ReadOnlyCollectionFormatter<>)},
              {typeof(IList<>), typeof(InterfaceListFormatter<>)},
              {typeof(ICollection<>), typeof(InterfaceCollectionFormatter<>)},
              {typeof(IEnumerable<>), typeof(InterfaceEnumerableFormatter<>)},
              {typeof(Dictionary<,>), typeof(DictionaryFormatter<,>)},
              {typeof(IDictionary<,>), typeof(InterfaceDictionaryFormatter<,>)},
              {typeof(SortedDictionary<,>), typeof(SortedDictionaryFormatter<,>)},
              {typeof(SortedList<,>), typeof(SortedListFormatter<,>)},
              {typeof(ILookup<,>), typeof(InterfaceLookupFormatter<,>)},
              {typeof(IGrouping<,>), typeof(InterfaceGroupingFormatter<,>)},
        };

        // Reduce IL2CPP code generate size(don't write long code in <T>)
        internal static object GetFormatter(Type t)
        {
            var ti = t.GetTypeInfo();

            if (t.IsArray)
            {
                var rank = t.GetArrayRank();
                if (rank == 1)
                {
                    if (t.GetElementType() == typeof(byte)) // byte[] is also supported in builtin formatter.
                    {
                        return ByteArrayFormatter.Instance;
                    }

                    return Activator.CreateInstance(typeof(ArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else if (rank == 2)
                {
                    return Activator.CreateInstance(typeof(TwoDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else if (rank == 3)
                {
                    return Activator.CreateInstance(typeof(ThreeDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else if (rank == 4)
                {
                    return Activator.CreateInstance(typeof(FourDimentionalArrayFormatter<>).MakeGenericType(t.GetElementType()));
                }
                else
                {
                    return null; // not supported built-in
                }
            }
            else if (ti.IsGenericType)
            {
                var genericType = ti.GetGenericTypeDefinition();
                var genericTypeInfo = genericType.GetTypeInfo();
                var isNullable = genericTypeInfo.IsNullable();
                var nullableElementType = isNullable ? ti.GenericTypeArguments[0] : null;

                if (genericType == typeof(KeyValuePair<,>))
                {
                    return CreateInstance(typeof(KeyValuePairFormatter<,>), ti.GenericTypeArguments);
                }
                else if (isNullable && nullableElementType.GetTypeInfo().IsConstructedGenericType() && nullableElementType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
                {
                    return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
                }

                // ArraySegement
                else if (genericType == typeof(ArraySegment<>))
                {
                    if (ti.GenericTypeArguments[0] == typeof(byte))
                    {
                        return ByteArraySegmentFormatter.Instance;
                    }
                    else
                    {
                        return CreateInstance(typeof(ArraySegmentFormatter<>), ti.GenericTypeArguments);
                    }
                }
                else if (isNullable && nullableElementType.GetTypeInfo().IsConstructedGenericType() && nullableElementType.GetGenericTypeDefinition() == typeof(ArraySegment<>))
                {
                    if (nullableElementType == typeof(ArraySegment<byte>))
                    {
                        return new StaticNullableFormatter<ArraySegment<byte>>(ByteArraySegmentFormatter.Instance);
                    }
                    else
                    {
                        return CreateInstance(typeof(NullableFormatter<>), new[] { nullableElementType });
                    }
                }

                // Mapped formatter
                else
                {
                    Type formatterType;
                    if (formatterMap.TryGetValue(genericType, out formatterType))
                    {
                        return CreateInstance(formatterType, ti.GenericTypeArguments);
                    }

                    // generic collection
                    else if (ti.GenericTypeArguments.Length == 1
                          && ti.ImplementedInterfaces.Any(x => x.GetTypeInfo().IsConstructedGenericType() && x.GetGenericTypeDefinition() == typeof(ICollection<>))
                          && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
                    {
                        var elemType = ti.GenericTypeArguments[0];
                        return CreateInstance(typeof(GenericCollectionFormatter<,>), new[] { elemType, t });
                    }
                    // generic dictionary
                    else if (ti.GenericTypeArguments.Length == 2
                          && ti.ImplementedInterfaces.Any(x => x.GetTypeInfo().IsConstructedGenericType() && x.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                          && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
                    {
                        var keyType = ti.GenericTypeArguments[0];
                        var valueType = ti.GenericTypeArguments[1];
                        return CreateInstance(typeof(GenericDictionaryFormatter<,,>), new[] { keyType, valueType, t });
                    }
                }
            }
            else
            {
                // NonGeneric Collection
                if (t == typeof(IList))
                {
                    return NonGenericInterfaceListFormatter.Instance;
                }
                else if (t == typeof(IDictionary))
                {
                    return NonGenericInterfaceDictionaryFormatter.Instance;
                }
                if (typeof(IList).GetTypeInfo().IsAssignableFrom(ti) && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
                {
                    return Activator.CreateInstance(typeof(NonGenericListFormatter<>).MakeGenericType(t));
                }
                else if (typeof(IDictionary).GetTypeInfo().IsAssignableFrom(ti) && ti.DeclaredConstructors.Any(x => x.GetParameters().Length == 0))
                {
                    return Activator.CreateInstance(typeof(NonGenericDictionaryFormatter<>).MakeGenericType(t));
                }
            }

            return null;
        }

        static object CreateInstance(Type genericType, Type[] genericTypeArguments, params object[] arguments)
        {
            return Activator.CreateInstance(genericType.MakeGenericType(genericTypeArguments), arguments);
        }
    }
}