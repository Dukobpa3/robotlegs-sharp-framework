//------------------------------------------------------------------------------
//  Copyright (c) 2014-2015 the original author or authors. All Rights Reserved. 
// 
//  NOTICE: You are permitted to use, modify, and distribute this file 
//  in accordance with the terms of the license agreement accompanying it. 
//------------------------------------------------------------------------------

﻿using System;
using NUnit.Framework;
using Robotlegs.Bender.Extensions.EventManagement.API;
using Robotlegs.Bender.Framework.Impl;
using System.Collections.Generic;

namespace Robotlegs.Bender.Extensions.EventManagement.Impl
{
	[TestFixture]
	public class LifecycleEventRelayTest
	{
		/*============================================================================*/
		/* Private Static Properties                                                  */
		/*============================================================================*/

		private static readonly List<LifecycleEvent.Type> LIFECYCLE_TYPES = new List<LifecycleEvent.Type>{
			LifecycleEvent.Type.PRE_INITIALIZE, LifecycleEvent.Type.INITIALIZE, LifecycleEvent.Type.POST_INITIALIZE,
			LifecycleEvent.Type.PRE_SUSPEND, LifecycleEvent.Type.SUSPEND, LifecycleEvent.Type.POST_SUSPEND,
			LifecycleEvent.Type.PRE_RESUME, LifecycleEvent.Type.RESUME, LifecycleEvent.Type.POST_RESUME,
			LifecycleEvent.Type.PRE_DESTROY, LifecycleEvent.Type.DESTROY, LifecycleEvent.Type.POST_DESTROY};

		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private Context context;

		private IEventDispatcher dispatcher;

		private LifecycleEventRelay subject;

		private List<object> reportedTypes;

		/*============================================================================*/
		/* Test Setup and Teardown                                                    */
		/*============================================================================*/

		[SetUp]
		public void before()
		{
			context = new Context();
			dispatcher = new EventDispatcher();
			subject = new LifecycleEventRelay(context, dispatcher);
			reportedTypes = new List<object>();
		}

		/*============================================================================*/
		/* Tests                                                                      */
		/*============================================================================*/

		[Test]
		public void state_change_is_relayed()
		{
			listenFor(new List<LifecycleEvent.Type>{LifecycleEvent.Type.STATE_CHANGE});
			context.Initialize();
			Assert.That(reportedTypes, Contains.Item(LifecycleEvent.Type.STATE_CHANGE));
		}

		[Test]
		public void lifecycle_events_are_relayed()
		{
			listenFor(LIFECYCLE_TYPES);
			context.Initialize();
			context.Suspend();
			context.Resume();
			context.Destroy();
			Assert.That(reportedTypes, Is.EquivalentTo(LIFECYCLE_TYPES));
		}

		[Test]
		public void lifecycle_events_are_NOT_relayed_after_destroy()
		{
			listenFor(LIFECYCLE_TYPES);
			subject.Destroy();
			context.Initialize();
			context.Suspend();
			context.Resume();
			context.Destroy();
			Assert.That(reportedTypes, Is.Empty);
		}

		/*============================================================================*/
		/* Private Functions                                                          */
		/*============================================================================*/

		private void listenFor(List<LifecycleEvent.Type> types)
		{
			foreach (LifecycleEvent.Type type in types)
			{
				dispatcher.AddEventListener(type, (Action<IEvent>)catchEvent);
			}
		}

		private void catchEvent(IEvent evt)
		{
			reportedTypes.Add(evt.type);
		}
	}
}

