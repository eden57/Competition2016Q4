package com.fh.entity;


/**
 * Model class of TBL_SYS_CONFIG.
 * 
 */
public class Config extends BaseEntity<Config> {
	/** serialVersionUID. */
	private static final long serialVersionUID = 1L;

	/** STAGE. */
	private String stage;

	/** TYPE. */
	private String type;

	/** KEY. */
	private String key;

	/** VALUE. */
	private String value;

	/** ENABLE. */
	private int enable;

	/**
	 * Constructor.
	 */
	public Config() {
		super();
	}

	public Config(String id){
		this.id = id;
	}

	/**
	 * Set the STAGE.
	 * 
	 * @param stage
	 *            STAGE
	 */
	public void setStage(String stage) {
		this.stage = stage;
	}

	/**
	 * Get the STAGE.
	 * 
	 * @return STAGE
	 */
	public String getStage() {
		return this.stage;
	}

	/**
	 * Set the TYPE.
	 * 
	 * @param type
	 *            TYPE
	 */
	public void setType(String type) {
		this.type = type;
	}

	/**
	 * Get the TYPE.
	 * 
	 * @return TYPE
	 */
	public String getType() {
		return this.type;
	}

	/**
	 * Set the KEY.
	 * 
	 * @param key
	 *            KEY
	 */
	public void setKey(String key) {
		this.key = key;
	}

	/**
	 * Get the KEY.
	 * 
	 * @return KEY
	 */
	public String getKey() {
		return this.key;
	}

	/**
	 * Set the VALUE.
	 * 
	 * @param value
	 *            VALUE
	 */
	public void setValue(String value) {
		this.value = value;
	}

	/**
	 * Get the VALUE.
	 * 
	 * @return VALUE
	 */
	public String getValue() {
		return this.value;
	}

	/**
	 * Set the ENABLE.
	 * 
	 * @param enable
	 *            ENABLE
	 */
	public void setEnable(Integer enable) {
		this.enable = enable;
	}

	/**
	 * Get the ENABLE.
	 * 
	 * @return ENABLE
	 */
	public Integer getEnable() {
		return this.enable;
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public int hashCode() {
		final int prime = 31;
		int result = 1;
		result = prime * result + ((id == null) ? 0 : id.hashCode());
		return result;
	}

	/**
	 * {@inheritDoc}
	 */
	@Override
	public boolean equals(Object obj) {
		if (this == obj) {
			return true;
		}
		if (obj == null) {
			return false;
		}
		if (getClass() != obj.getClass()) {
			return false;
		}
		Config other = (Config) obj;
		if (id == null) {
			if (other.id != null) {
				return false;
			}
		} else if (!id.equals(other.id)) {
			return false;
		}
		return true;
	}
}
