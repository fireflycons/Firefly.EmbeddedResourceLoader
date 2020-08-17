namespace Firefly.EmbeddedResourceLoader
{
    using System;
    using System.Globalization;
    using System.Reflection;

    /// <summary>
    /// <see cref="Binder"/> implementation to ensure that when the resource loader asks for a constructor
    /// or method accepting a particular type (in our case <see cref="string"/> or <see cref="System.IO.Stream"/>,
    /// a fallback method that accepts <see cref="object"/> is not returned when the method accepting the requested type
    /// is not present.
    /// </summary>
    internal class ResourceLoadBinder : Binder
    {
        /// <summary>
        /// Selects a field from the given set of fields, based on the specified criteria.
        /// </summary>
        /// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags"></see> values.</param>
        /// <param name="match">The set of fields that are candidates for matching. For example, when a <see cref="T:System.Reflection.Binder"></see> object is used by <see cref="Overload:System.Type.InvokeMember"></see>, this parameter specifies the set of fields that reflection has determined to be possible matches, typically because they have the correct member name. The default implementation provided by <see cref="P:System.Type.DefaultBinder"></see> changes the order of this array.</param>
        /// <param name="value">The field value used to locate a matching field.</param>
        /// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo"></see> that is used to control the coercion of data types, in binder implementations that coerce types. If culture is null, the <see cref="T:System.Globalization.CultureInfo"></see> for the current thread is used.   Note   For example, if a binder implementation allows coercion of string values to numeric types, this parameter is necessary to convert a String that represents 1000 to a Double value, because 1000 is represented differently by different cultures. The default binder does not do such string coercions.</param>
        /// <returns>
        /// The matching field.
        /// </returns>
        /// <exception cref="NotImplementedException">Method should not be needed for this implementation</exception>
        public override FieldInfo BindToField(
            BindingFlags bindingAttr,
            FieldInfo[] match,
            object value,
            CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects a method to invoke from the given set of methods, based on the supplied arguments.
        /// </summary>
        /// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags"></see> values.</param>
        /// <param name="match">The set of methods that are candidates for matching. For example, when a <see cref="T:System.Reflection.Binder"></see> object is used by <see cref="Overload:System.Type.InvokeMember"></see>, this parameter specifies the set of methods that reflection has determined to be possible matches, typically because they have the correct member name. The default implementation provided by <see cref="P:System.Type.DefaultBinder"></see> changes the order of this array.</param>
        /// <param name="args">The arguments that are passed in. The binder can change the order of the arguments in this array; for example, the default binder changes the order of arguments if the names parameter is used to specify an order other than positional order. If a binder implementation coerces argument types, the types and values of the arguments can be changed as well.</param>
        /// <param name="modifiers">An array of parameter modifiers that enable binding to work with parameter signatures in which the types have been modified. The default binder implementation does not use this parameter.</param>
        /// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo"></see> that is used to control the coercion of data types, in binder implementations that coerce types. If culture is null, the <see cref="T:System.Globalization.CultureInfo"></see> for the current thread is used.   Note   For example, if a binder implementation allows coercion of string values to numeric types, this parameter is necessary to convert a String that represents 1000 to a Double value, because 1000 is represented differently by different cultures. The default binder does not do such string coercions.</param>
        /// <param name="names">The parameter names, if parameter names are to be considered when matching, or null if arguments are to be treated as purely positional. For example, parameter names must be used if arguments are not supplied in positional order.</param>
        /// <param name="state">After the method returns, state contains a binder-provided object that keeps track of argument reordering. The binder creates this object, and the binder is the sole consumer of this object. If state is not null when BindToMethod returns, you must pass state to the <see cref="M:System.Reflection.Binder.ReorderArgumentArray(System.Object[]@,System.Object)"></see> method if you want to restore args to its original order, for example, so that you can retrieve the values of ref parameters (ByRef parameters in Visual Basic).</param>
        /// <returns>
        /// The matching method.
        /// </returns>
        /// <exception cref="NotImplementedException">Method should not be needed for this implementation</exception>
        public override MethodBase BindToMethod(
            BindingFlags bindingAttr,
            MethodBase[] match,
            ref object[] args,
            ParameterModifier[] modifiers,
            CultureInfo culture,
            string[] names,
            out object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Changes the type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="myChangeType">Type of my change.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>Type converted <paramref name="value"/></returns>
        /// <exception cref="NotImplementedException">Method should not be needed for this implementation</exception>
        public override object ChangeType(object value, Type myChangeType, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Upon returning from <see cref="M:System.Reflection.Binder.BindToMethod(System.Reflection.BindingFlags,System.Reflection.MethodBase[],System.Object[]@,System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[],System.Object@)"></see>, restores the <paramref name="args">args</paramref> argument to what it was when it came from BindToMethod.
        /// </summary>
        /// <param name="args">The actual arguments that are passed in. Both the types and values of the arguments can be changed.</param>
        /// <param name="state">A binder-provided object that keeps track of argument reordering.</param>
        /// <exception cref="NotImplementedException">Method should not be needed for this implementation</exception>
        public override void ReorderArgumentArray(ref object[] args, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Selects a method from the given set of methods, based on the argument type.
        /// </summary>
        /// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags"></see> values.</param>
        /// <param name="match">The set of methods that are candidates for matching. For example, when a <see cref="T:System.Reflection.Binder"></see> object is used by <see cref="Overload:System.Type.InvokeMember"></see>, this parameter specifies the set of methods that reflection has determined to be possible matches, typically because they have the correct member name. The default implementation provided by <see cref="P:System.Type.DefaultBinder"></see> changes the order of this array.</param>
        /// <param name="types">The parameter types used to locate a matching method.</param>
        /// <param name="modifiers">An array of parameter modifiers that enable binding to work with parameter signatures in which the types have been modified.</param>
        /// <returns>
        /// The matching method, if found; otherwise, null.
        /// </returns>
        /// <exception cref="ArgumentNullException">match is null</exception>
        public override MethodBase SelectMethod(
            BindingFlags bindingAttr,
            MethodBase[] match,
            Type[] types,
            ParameterModifier[] modifiers)
        {
            if (match == null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            foreach (var method in match)
            {
                // Count the number of parameters that match.
                var count = 0;
                var parameters = method.GetParameters();

                // Go on to the next method if the number of parameters do not match.
                if (types.Length != parameters.Length)
                {
                    continue;
                }

                // Match each of the parameters that the user expects the method to have.
                for (var j = 0; j < types.Length; j++)
                {
                    // Determine whether the types specified by the user can be converted to parameter type.
                    // Explicitly ignore method parameters of type object as everything is a subclass of this
                    // and we will most likely get an exception or unexpected behaviour.
                    if (parameters[j].ParameterType != typeof(object)
                        && (types[j].IsSubclassOf(parameters[j].ParameterType)
                            || types[j].IsAssignableFrom(parameters[j].ParameterType)))
                    {
                        count += 1;
                    }
                    else
                    {
                        break;
                    }
                }

                // Determine whether the method has been found.
                if (count == types.Length)
                {
                    return method;
                }
            }

            return null;
        }

        /// <summary>
        /// Selects a property from the given set of properties, based on the specified criteria.
        /// </summary>
        /// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags"></see> values.</param>
        /// <param name="match">The set of properties that are candidates for matching. For example, when a <see cref="T:System.Reflection.Binder"></see> object is used by <see cref="Overload:System.Type.InvokeMember"></see>, this parameter specifies the set of properties that reflection has determined to be possible matches, typically because they have the correct member name. The default implementation provided by <see cref="P:System.Type.DefaultBinder"></see> changes the order of this array.</param>
        /// <param name="returnType">The return value the matching property must have.</param>
        /// <param name="indexes">The index types of the property being searched for. Used for index properties such as the indexer for a class.</param>
        /// <param name="modifiers">An array of parameter modifiers that enable binding to work with parameter signatures in which the types have been modified.</param>
        /// <returns>
        /// The matching property.
        /// </returns>
        /// <exception cref="NotImplementedException">Method should not be needed for this implementation</exception>
        public override PropertyInfo SelectProperty(
            BindingFlags bindingAttr,
            PropertyInfo[] match,
            Type returnType,
            Type[] indexes,
            ParameterModifier[] modifiers)
        {
            throw new NotImplementedException();
        }
    }
}