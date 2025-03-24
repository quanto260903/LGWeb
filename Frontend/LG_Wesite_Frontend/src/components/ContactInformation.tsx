'use client';

import React, { useState, useEffect } from 'react';
import '../app/css/abstracts/_variables.scss';
import '../app/css/components/_buttons.scss';
import '../app/css/pages/_about.scss';

import { BlogCategoryType } from '@/type/BlogCategory';
import { getContactData } from '@/api/contact';
import { getContactDataBottom } from '@/api/contact';

const AboutUs = ({ showButton = true }) => {
  const [contactData, setContactData] = useState<BlogCategoryType[]>([]);
  const [contactDataBottom, setContactDataBottom] = useState<BlogCategoryType[]>([]);


  useEffect(() => {
    const loadContactData = async () => {
      const result = await getContactData();
      setContactData(result);
    };
    loadContactData();
  }, []);


  useEffect(() => {
    const loadContactDataBottom = async () => {
      const result = await getContactDataBottom();
      setContactDataBottom(result);
    };
    loadContactDataBottom();
  }, []);

  return (
    <>
      <div className="contactInfo">
        {contactData.map((item, index) => (
          <div key={index} className="contactItem">
            <div className="contactInfo-section-icon">
              <img
                src={item.imageUrl || '/picture/NoImage.png'}
                alt={item.name || 'Image'}
                className="icon"
                style={{ width: 'auto', height: 'auto' }}
              />
            </div>
            <div className="contactInfo-section-text">
              <h3 className="tl41">{item.name}</h3>
              <p className="tl4">{item.description}</p>
            </div>
          </div>
        ))}

        <div className="socialIcons">
          <h3 className="follow tl3">Follow us</h3>
          <div className="icons">
            {contactDataBottom.map((item, index) => (
              <img
                key={index}
                src={item.imageUrl || '/picture/NoImage.png'}
                alt={item.name || 'Image'}
                style={{ width: 'auto', height: 'auto' }}
              />
            ))}
          </div>
        </div>
      </div>
    </>
  );
};

export default AboutUs;
