'use client';

import React, { useState, useEffect } from 'react';
import Link from 'next/link';
import Image from 'next/image';

// Import các hình ảnh
import icoFb from './../app/images/ico-fb.png';
import icoLinkedIn from './../app/images/ico-linkedin.png';
import icoYoutube from './../app/images/ico-youtube.png';


import { BlogCategoryType } from '@/type/BlogCategory';
import { getContactDataBottom } from '@/api/contact';


// Component Policy
const Policy: React.FC = () => {
  const [contactDataBottom, setContactDataBottom] = useState<BlogCategoryType[]>([]);

  useEffect(() => {
    const loadContactDataBottom = async () => {
      const result = await getContactDataBottom();
      setContactDataBottom(result);
    };
    loadContactDataBottom();
  }, []);
  return (
    <div className="policy">
      <div className="policy-a wrp">
        <div className="col-left">
          <p>
            Add-On Development © 2024 | <Link href={'/'}>Privacy Policy</Link>
          </p>
        </div>
        <div className="col-right">
          <ul>
            {/* {contactDataBottom.map((item, index) => (
              <li>
               <img
                key={index}
                src={item.imageUrl || '/picture/NoImage.png'}
                alt={item.name || 'Image'}
                style={{ width: '24px', height: 'auto' }}
              />
            </li>
            ))} */}
          </ul>
        </div>
      </div>
    </div>
  );
};

export default Policy;
