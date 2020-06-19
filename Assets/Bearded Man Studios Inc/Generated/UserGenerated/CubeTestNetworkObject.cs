using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.3]")]
	public partial class CubeTestNetworkObject : NetworkObject
	{
		public const int IDENTITY = 6;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _pos;
		public event FieldEvent<Vector3> posChanged;
		public InterpolateVector3 posInterpolation = new InterpolateVector3() { LerpT = 0.3f, Enabled = true };
		public Vector3 pos
		{
			get { return _pos; }
			set
			{
				// Don't do anything if the value is the same
				if (_pos == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_pos = value;
				hasDirtyFields = true;
			}
		}

		public void SetposDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_pos(ulong timestep)
		{
			if (posChanged != null) posChanged(_pos, timestep);
			if (fieldAltered != null) fieldAltered("pos", _pos, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			posInterpolation.current = posInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _pos);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_pos = UnityObjectMapper.Instance.Map<Vector3>(payload);
			posInterpolation.current = _pos;
			posInterpolation.target = _pos;
			RunChange_pos(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _pos);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (posInterpolation.Enabled)
				{
					posInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					posInterpolation.Timestep = timestep;
				}
				else
				{
					_pos = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_pos(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (posInterpolation.Enabled && !posInterpolation.current.UnityNear(posInterpolation.target, 0.0015f))
			{
				_pos = (Vector3)posInterpolation.Interpolate();
				//RunChange_pos(posInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public CubeTestNetworkObject() : base() { Initialize(); }
		public CubeTestNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public CubeTestNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
