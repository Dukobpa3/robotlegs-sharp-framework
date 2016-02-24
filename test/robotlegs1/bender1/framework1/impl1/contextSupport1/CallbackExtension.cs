//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;
using Robotlegs.Bender.Framework.API;

namespace Robotlegs.Bender.Framework.Impl.ContextSupport
{
	public class CallbackExtension : IExtension
	{
		/*============================================================================*/
		/* Public Static Properties                                                   */
		/*============================================================================*/

		public static Action<IContext> staticCallback;

		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private Action<IContext> _callback;

		/*============================================================================*/
		/* Constructor                                                                */
		/*============================================================================*/

		public CallbackExtension(Action<IContext> callback = null)
		{
			_callback = callback == null ? staticCallback : callback;
			staticCallback = null;
		}

		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public virtual void Extend(IContext context)
		{
			_callback (context);
		}
	}
}

