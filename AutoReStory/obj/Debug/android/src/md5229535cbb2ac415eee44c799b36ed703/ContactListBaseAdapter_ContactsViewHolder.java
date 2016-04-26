package md5229535cbb2ac415eee44c799b36ed703;


public class ContactListBaseAdapter_ContactsViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AutoReStory.ContactListBaseAdapter+ContactsViewHolder, AutoReStory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ContactListBaseAdapter_ContactsViewHolder.class, __md_methods);
	}


	public ContactListBaseAdapter_ContactsViewHolder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ContactListBaseAdapter_ContactsViewHolder.class)
			mono.android.TypeManager.Activate ("AutoReStory.ContactListBaseAdapter+ContactsViewHolder, AutoReStory, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	java.util.ArrayList refList;
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
