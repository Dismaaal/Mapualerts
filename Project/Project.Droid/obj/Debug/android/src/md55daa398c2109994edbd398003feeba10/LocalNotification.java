package md55daa398c2109994edbd398003feeba10;


public class LocalNotification
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Project.Droid.LocalNotification, Project.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LocalNotification.class, __md_methods);
	}


	public LocalNotification () throws java.lang.Throwable
	{
		super ();
		if (getClass () == LocalNotification.class)
			mono.android.TypeManager.Activate ("Project.Droid.LocalNotification, Project.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
