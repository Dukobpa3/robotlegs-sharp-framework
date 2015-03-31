//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using UnityEngine;
using System.Collections;
using robotlegs.bender.framework.api;
using robotlegs.bender.platforms.unity.extensions.contextview;
using robotlegs.bender.extensions.viewManager;

namespace robotlegs.bender.bundles
{
	public class UnityFallbackBundle : IBundle
	{
		/*============================================================================*/
		/* Public Functions                                                           */
		/*============================================================================*/

		public void Extend (IContext context)
		{
			context.Configure(typeof(UnityFallbackStageCrawlerConfig));
			context.Configure(typeof(FallbackContainerConfig));
		}
	}
}