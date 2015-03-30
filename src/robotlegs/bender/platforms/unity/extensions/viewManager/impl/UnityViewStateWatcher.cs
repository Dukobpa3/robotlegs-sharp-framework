using UnityEngine;
using System;
using robotlegs.bender.extensions.viewManager.api;


namespace robotlegs.bender.platforms.unity.extensions.viewManager.impl
{
	public class UnityViewStateWatcher : MonoBehaviour, IViewStateWatcher
	{
		/*============================================================================*/
		/* Public Properties                                                          */
		/*============================================================================*/

		public event Action<object> added
		{
			add
			{
				_added += value;
			}
			remove 
			{
				_added -= value;
			}
		}

		public event Action<object> removed
		{
			add
			{
				_removed += value;
			}
			remove 
			{
				_removed -= value;
			}
		}

		public event Action<object> disabled
		{
			add
			{
				_disabled += value;
			}
			remove 
			{
				_disabled -= value;
			}
		}

		#pragma warning disable 0108
		public event Action<object> enabled
		{
			add
			{
				_enabled += value;
			}
			remove 
			{
				_enabled -= value;
			}
		}
		#pragma warning restore 0108

		public GameObject target;

		public bool isAdded 
		{ 
			get 
			{ 
				return _isAdded; 
			} 
		}

		/*============================================================================*/
		/* Private Properties                                                         */
		/*============================================================================*/

		private Action<object> _added;

		private Action<object> _removed;

		private Action<object> _disabled;

		private Action<object> _enabled;

		private bool _isAdded;

		private bool _hasBeenDisabled;

		/*============================================================================*/
		/* Protected Functions                                                        */
		/*============================================================================*/

		protected virtual void Start()
		{
			_isAdded = true;
			if (_added != null)
			{
				_added (target);
			}
		}

		protected virtual void OnEnable()
		{
			if(_hasBeenDisabled)
			{
				_hasBeenDisabled = false;
				if (_enabled != null)
				{
					_enabled (target);
				}
			}
		}

		protected virtual void OnDisable()
		{
			_hasBeenDisabled = true;
			if (_disabled != null)
			{
				_disabled (target);
			}
		}

		protected virtual void OnDestroy()
		{
			_isAdded = false;
			if (_removed != null)
			{
				_removed (target);
			}
		}
	}
}

